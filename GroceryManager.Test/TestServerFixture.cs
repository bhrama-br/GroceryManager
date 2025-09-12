using GroceryManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Reflection.PortableExecutable;

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

            var scopeFactory = TestServer.Services.GetService<IServiceScopeFactory>();
            if (scopeFactory is null)
            {
                throw new Exception("ScopeFactory is null");
            }

            var scope = scopeFactory.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            if (scopeFactory is null)
            {
                throw new Exception("ScopeFactory is null");
            }
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}