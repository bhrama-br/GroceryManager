using System.Text.Json;
using System.Text.Json.Serialization;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Revenue;

namespace GroceryManager.Test.Revenues
{
    [Collection("Integration Tests")]
    [TestCaseOrderer("GroceryManager.Api.IntegrationTests.PriorityOrderer", "GroceryManager.Api.IntegrationTests")]
    public class RevenueTests : TestBase
    {

        public RevenueTests(TestServerFixture fixture) : base(fixture)
        {
            var revenue = new Revenue { Id = 1, Name = "Test Revenue 1", Ingredients = "Ingredient1, Ingredient2", PreparationMode = "Test Preparation", ImageUrl = "http://example.com/image.jpg", Type = "Dessert", IngredientNames = new List<string> { "Ingredient1", "Ingredient2" } };

            _fixture.DbContext.Revenues.Add(revenue);
            _fixture.DbContext.SaveChanges();
        }

        [Fact]
        public async Task GetRevenues_ReturnsList()
        {
            var response = await _client.GetAsync("/api/Revenue");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var revenues = JsonSerializer.Deserialize<List<GetRevenuesDto>>(responseContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            });

            Assert.NotNull(revenues);
            Assert.NotEmpty(revenues);
        }


        [Fact]
        public async Task GetRevenue_ReturnsSingle()
        {

            var response = await _client.GetAsync($"/api/Revenue?name=Revenue");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<GetRevenuesDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(dto);
            Assert.Equal("Test Revenue 1", dto.Name);
        }
    }
}