

using GroceryManager.Database;
using GroceryManager.Database.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebMotions.Fake.Authentication.JwtBearer;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GroceryManager.Test
{
    public class TestServer : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptors = services
                   .Where(d => d.ServiceType == typeof(DbContextOptions)
                            || d.ServiceType == typeof(DbContextOptions<DataContext>))
                   .ToList();

                foreach (var descriptor in dbContextDescriptors)
                    services.Remove(descriptor);

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");


                    options.ConfigureWarnings(w =>
                        w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
                });

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                })
                .AddFakeJwtBearer();
            });
        }
    }
}