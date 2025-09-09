using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GroceryManager.Configurations
{
  public static class SwaggerConfig
  {
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Grocery Manager API",
          Version = "v1"
        });
      });

      return services;
    }
  }
}
