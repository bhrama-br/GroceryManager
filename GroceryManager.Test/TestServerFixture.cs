using GroceryManager.Database;
using GroceryManager.Database.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace GroceryManager.Test
{
    public class TestServerFixture : IDisposable
    {
        public TestServer TestServer { get; private set; }
        public HttpClient Client { get; private set; }
        public DataContext DbContext { get; private set; }

        public TestServerFixture()
        {
            TestServer = new TestServer();
            Client = TestServer.CreateClient();
            SetAuthentication();

            var scopeFactory = TestServer.Services.GetRequiredService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();

            DbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            // opcional: limpa a BD antes de cada execução
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();


            if (!DbContext.ShoppingLists.Any())
            {
                DbContext.ShoppingLists.Add(new ShoppingList { Id = 1, IsPurchased = false, CreatedAt = DateTime.Now });
                DbContext.SaveChanges();
            }

            if (!DbContext.Supermarkets.Any())
            {
                DbContext.Supermarkets.Add(new Supermarket { Id = 1, Name = "Supermarket 1" });
                DbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
        }

        private void SetAuthentication()
        {
            var claims = new Dictionary<string, object>();
            Client.SetFakeBearerToken(claims);
        }
    }
}
