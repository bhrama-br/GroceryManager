using System.Text.Json;
using System.Text.Json.Serialization;
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
        public async Task GetAllShoppingList()
        {
            var response = await _client.GetAsync("/Supermarket/GetAll");

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

    }
}