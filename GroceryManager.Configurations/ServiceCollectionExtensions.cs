using GroceryManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace GroceryManager.Configurations
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddGroceryConfigurations(
        this IServiceCollection services, IConfiguration configuration)
    {
      var connectionString = configuration.GetConnectionString("DefaultConnection");
      if (string.IsNullOrEmpty(connectionString))
      {
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
      }

      // Controllers + FluentValidation
      services.AddControllers();

      // Swagger
      services.AddEndpointsApiExplorer();

      // DbContext (SQL Server)
      services.AddDbContext<DataContext>(options =>
          options.UseSqlServer(connectionString,
            b => b.MigrationsAssembly("GroceryManager.Database.Migrations")));

      // Register other services
      // services.AddScoped<IItemService, ItemService>();

      services.AddSwaggerConfiguration();
      services.AddAutoMapper(typeof(MappingProfile).Assembly);

      return services;
    }
  }
}
