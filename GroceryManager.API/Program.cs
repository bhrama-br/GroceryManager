global using GroceryManager.Models;
global using GroceryManager.Services.ShoppingListService;
global using GroceryManager.Services.ItemService;
using GroceryManager.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Usa a extensão centralizada no projeto Configurations
builder.Services.AddGroceryConfigurations(builder.Configuration);

builder.Services.AddScoped<IShoppingListService, ShoppingListService>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
