global using GroceryManager.Models;
global using GroceryManager.Services.ShoppingLists;
global using GroceryManager.Services.Items;
global using GroceryManager.Services.Users;
using GroceryManager.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add configurations to project
builder.Services.AddGroceryConfigurations(builder.Configuration);

builder.Services.AddScoped<IShoppingListService, ShoppingListService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IAuthService, AuthService>();


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
