using GroceryManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using GroceryManager.Auth.Services;
using System.Text;
using GroceryManager.Auth.Models;

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


      services.AddSwaggerConfiguration();
      services.AddAutoMapper(typeof(MappingProfile).Assembly);

      // JWT Settings
      services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

      var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
            options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = jwtSettings.Issuer,
              ValidAudience = jwtSettings.Audience,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
          });

      services.AddScoped<ITokenService, TokenService>();

      return services;
    }
  }
}
