using System.Text.Json;
using System.Text.Json.Serialization;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Item;

namespace GroceryManager.Test.Items
{
    [Collection("Integration Tests")]
    [TestCaseOrderer("GroceryManager.Api.IntegrationTests.PriorityOrderer", "GroceryManager.Api.IntegrationTests")]
    public class ItemTests : TestBase
    {

        public ItemTests(TestServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetItems_ReturnsList()
        {
            var item = new Item { Id = 100, Name = "Item 1", Quantity = 1, ShoppingList = _fixture.DbContext.ShoppingLists.First() };
            _fixture.DbContext.Items.Add(item);
            _fixture.DbContext.SaveChanges();
            var response = await _client.GetAsync("/api/Item");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var items = JsonSerializer.Deserialize<List<GetItemDto>>(responseContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            });

            Assert.NotNull(items);
            Assert.NotEmpty(items);
        }


        [Fact]
        public async Task GetItem_ReturnsSingle()
        {
            var item = new Item { Id = 2, Name = "Item 1", Quantity = 1 };
            _fixture.DbContext.Items.Add(item);
            _fixture.DbContext.SaveChanges();

            var response = await _client.GetAsync($"/api/Item/{item.Id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<GetItemDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(dto);
            Assert.Equal(item.Id, dto.Id);
        }

        [Fact]
        public async Task AddItem_CreatesNew()
        {
            var addDto = new AddItemDto { Name = "Item 1", Quantity = 1, ShoppingListId = 1, SupermarketId = 1 };
            var json = JsonSerializer.Serialize(addDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Item", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<GetItemDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(item);
            Assert.Equal(addDto.Name, item.Name);
            Assert.Equal(addDto.Quantity, item.Quantity);
        }

        [Fact]
        public async Task UpdateItem_UpdatesExisting()
        {
            var item = new Item { Id = 11, Name = "Item 1", Quantity = 10 };
            _fixture.DbContext.Items.Add(item);
            _fixture.DbContext.SaveChanges();

            var updateDto = new UpdateItemDto { Id = item.Id, Name = "Updated Item", Quantity = 2 };
            var json = JsonSerializer.Serialize(updateDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"/api/Item", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<GetItemDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(dto);
            Assert.Equal(updateDto.Name, dto.Name);
            Assert.Equal(updateDto.Quantity, dto.Quantity);
        }

    }
}