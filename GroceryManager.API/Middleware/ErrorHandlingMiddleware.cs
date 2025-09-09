using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MyApiProject.Api.Middleware;

public class ErrorHandlingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ErrorHandlingMiddleware> _logger;

  public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An unhandled exception occurred.");
      context.Response.StatusCode = StatusCodes.Status500InternalServerError;
      await context.Response.WriteAsync("An unexpected error occurred.");
    }
  }
}