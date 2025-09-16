using GroceryManager.Database;
using Moq;
using System.Text.Json;

namespace GroceryManager.Test
{
    public class TestBase : IClassFixture<TestServerFixture>, IAsyncLifetime
    {
        protected readonly TestServerFixture _fixture;
        protected readonly HttpClient _client;
        protected readonly DataContext _db;

        protected TestBase(TestServerFixture fixture)
        {
            _fixture = fixture;
            _client = _fixture.Client;
            _db = _fixture.DbContext;
        }

        public virtual Task InitializeAsync()
        {
            // Perform any additional initialization, if required.
            return Task.CompletedTask;
        }

        public virtual Task DisposeAsync()
        {
            // Perform any cleanup, if required.
            return Task.CompletedTask;
        }
    }
}