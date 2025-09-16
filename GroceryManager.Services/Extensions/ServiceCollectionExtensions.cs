using GroceryManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GroceryManager.Auth.Models;
using GroceryManager.Configurations;
using FluentValidation;
using GroceryManager.Services.ShoppingLists;
using GroceryManager.Services.Items;
using GroceryManager.Services.Users;
using GroceryManager.Services.Supermarkets;

namespace GroceryManager.Services.Extensions
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


      services.AddTransient<DatabaseMigrations>();




      services.AddSwaggerConfiguration();
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);


      services.AddScoped<IShoppingListService, ShoppingListService>();
      services.AddScoped<IItemService, ItemService>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<ISupermarketService, SupermarketService>();

      // JWT Settings
      services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

      var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
      if (jwtSettings is null)
      {
        throw new InvalidOperationException("JWT settings not found in configuration.");
      }

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ValidateIssuer = false,
            ValidateAudience = false
          };
        });

      return services;
    }
  }
}
