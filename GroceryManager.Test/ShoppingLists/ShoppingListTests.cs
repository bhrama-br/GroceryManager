using System.Text.Json;
using System.Text.Json.Serialization;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.ShoppingList;

namespace GroceryManager.Test.ShoppingLists
{
    [Collection("Integration Tests")]
    [TestCaseOrderer("GroceryManager.Api.IntegrationTests.PriorityOrderer", "GroceryManager.Api.IntegrationTests")]
    public class ShoppingListTests : TestBase
    {

        public ShoppingListTests(TestServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetShoppingLists_ReturnsList()
        {
            var response = await _client.GetAsync("/api/ShoppingList");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var shoppingLists = JsonSerializer.Deserialize<List<GetShoppingListDto>>(responseContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            });

            Assert.NotNull(shoppingLists);
            Assert.NotEmpty(shoppingLists);
        }


        [Fact]
        public async Task GetShoppingList_ReturnsSingle()
        {
            var shoppingList = new ShoppingList { Id = 2, IsPurchased = false, CreatedAt = DateTime.Now };
            _fixture.DbContext.ShoppingLists.Add(shoppingList);
            _fixture.DbContext.SaveChanges();

            var response = await _client.GetAsync($"/api/ShoppingList/{shoppingList.Id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<GetShoppingListDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(dto);
            Assert.Equal(shoppingList.Id, dto.Id);
        }

        [Fact]
        public async Task AddShoppingList_CreatesNew()
        {
            var addDto = new AddShoppingListDto { IsPurchased = false };
            var json = JsonSerializer.Serialize(addDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/ShoppingList", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var lists = JsonSerializer.Deserialize<List<GetShoppingListDto>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(lists);
        }

        [Fact]
        public async Task UpdateShoppingList_UpdatesExisting()
        {
            var shoppingList = new ShoppingList { Id = 13, IsPurchased = false, CreatedAt = DateTime.Now };
            _fixture.DbContext.ShoppingLists.Add(shoppingList);
            _fixture.DbContext.SaveChanges();

            var updateDto = new UpdateShoppingListDto { Id = shoppingList.Id, IsPurchased = true };
            var json = JsonSerializer.Serialize(updateDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/ShoppingList", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<GetShoppingListDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(dto);
            Assert.Equal(updateDto.IsPurchased, dto.IsPurchased);
        }

    }
}