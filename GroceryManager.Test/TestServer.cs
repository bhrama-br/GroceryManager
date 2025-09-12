
using GroceryManager.Database;
using GroceryManager.Database.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Net.Sockets;
using WebMotions.Fake.Authentication.JwtBearer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GroceryManager.Test
{
    public class TestServer : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DataContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });


                var supermarket = new Supermarket
                {
                    Id = 1,
                    Name = "Test Supermarket"
                };
                var supermarkets = new List<Supermarket> { supermarket }.AsQueryable();

                var shoppingList = new ShoppingList
                {
                    Id = 1,
                    IsPurchased = false
                };
                var shoppingLists = new List<ShoppingList> { shoppingList }.AsQueryable();

                var item = new Item
                {
                    Id = 1,
                    Name = "Test Item",
                    Supermarket = supermarket,
                    ShoppingList = shoppingList
                };

                var mockSetSupermarket = new Mock<DbSet<Supermarket>>();
                mockSetSupermarket.As<IQueryable<Supermarket>>().Setup(m => m.Provider).Returns(supermarkets.Provider);
                mockSetSupermarket.As<IQueryable<Supermarket>>().Setup(m => m.Expression).Returns(supermarkets.Expression);
                mockSetSupermarket.As<IQueryable<Supermarket>>().Setup(m => m.ElementType).Returns(supermarkets.ElementType);
                mockSetSupermarket.As<IQueryable<Supermarket>>().Setup(m => m.GetEnumerator()).Returns(supermarkets.GetEnumerator());

                var mockShoppingList = new Mock<DbSet<ShoppingList>>();
                mockShoppingList.As<IQueryable<ShoppingList>>().Setup(m => m.Provider).Returns(shoppingLists.Provider);
                mockShoppingList.As<IQueryable<ShoppingList>>().Setup(m => m.Expression).Returns(shoppingLists.Expression);
                mockShoppingList.As<IQueryable<ShoppingList>>().Setup(m => m.ElementType).Returns(shoppingLists.ElementType);
                mockShoppingList.As<IQueryable<ShoppingList>>().Setup(m => m.GetEnumerator()).Returns(shoppingLists.GetEnumerator());

                var mockItem = new Mock<DbSet<Item>>();
                mockItem.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(new List<Item> { item }.AsQueryable().Provider);
                mockItem.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(new List<Item> { item }.AsQueryable().Expression);
                mockItem.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(new List<Item> { item }.AsQueryable().ElementType);
                mockItem.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(new List<Item> { item }.AsQueryable().GetEnumerator());

                var mockContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
                mockContext.Setup(c => c.Supermarkets).Returns(mockSetSupermarket.Object);
                mockContext.Setup(c => c.ShoppingLists).Returns(mockShoppingList.Object);
                mockContext.Setup(c => c.Items).Returns(mockItem.Object);

                services.AddSingleton(mockContext.Object);

                services.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme).AddFakeJwtBearer();
            });
        }
    }
}