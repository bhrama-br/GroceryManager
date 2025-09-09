using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace GroceryManager.Configurations
{
  public static class SwaggerConfig
  {
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
          Description = """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
          In = ParameterLocation.Header,
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
        });

        c.OperationFilter<SecurityRequirementsOperationFilter>();
      });
      services.AddHttpContextAccessor();


      return services;
    }
  }
}
