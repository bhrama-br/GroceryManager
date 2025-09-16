using System.Text.Json;
using System.Text.Json.Serialization;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Supermarket;

namespace GroceryManager.Test.Supermarkets
{
    [Collection("Integration Tests")]
    [TestCaseOrderer("GroceryManager.Api.IntegrationTests.PriorityOrderer", "GroceryManager.Api.IntegrationTests")]
    public class SupermarketTests : TestBase
    {

        public SupermarketTests(TestServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetSupermarkets_ReturnsList()
        {
            var supermarket = new Supermarket { Id = 33, Name = "Supermarket 1"};
            _fixture.DbContext.Supermarkets.Add(supermarket);
            _fixture.DbContext.SaveChanges();

            var response = await _client.GetAsync("/api/Supermarket");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var supermarkets = JsonSerializer.Deserialize<List<GetSupermarketDto>>(responseContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            });

            Assert.NotNull(supermarkets);
            Assert.NotEmpty(supermarkets);
        }


        [Fact]
        public async Task GetSupermarket_ReturnsSingle()
        {
            var supermarket = new Supermarket { Id = 2, Name = "Supermarket 1" };
            _fixture.DbContext.Supermarkets.Add(supermarket);
            _fixture.DbContext.SaveChanges();

            var response = await _client.GetAsync($"/api/Supermarket/{supermarket.Id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<GetSupermarketDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(dto);
            Assert.Equal(supermarket.Id, dto.Id);
        }

        [Fact]
        public async Task AddSupermarket_CreatesNew()
        {
            var addDto = new AddSupermarketDto { Name = "Supermarket 1" };
            var json = JsonSerializer.Serialize(addDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Supermarket", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var lists = JsonSerializer.Deserialize<List<GetSupermarketDto>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(lists);
        }

        [Fact]
        public async Task UpdateSupermarket_UpdatesExisting()
        {
            var supermarket = new Supermarket { Id = 10, Name = "Supermarket 1" };
            _fixture.DbContext.Supermarkets.Add(supermarket);
            _fixture.DbContext.SaveChanges();

            var updateDto = new UpdateSupermarketDto { Id = supermarket.Id, Name = "Updated Supermarket" };
            var json = JsonSerializer.Serialize(updateDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"/api/Supermarket", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<GetSupermarketDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(dto);
            Assert.Equal(updateDto.Name, dto.Name);
        }

    }
}