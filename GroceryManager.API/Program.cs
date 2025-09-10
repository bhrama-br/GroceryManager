global using GroceryManager.Models;
global using GroceryManager.Services.ShoppingListService;
global using GroceryManager.Services.ItemService;
using GroceryManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configurations to project
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
