

global using GroceryManager.Services.ShoppingLists;
global using GroceryManager.Services.Items;
global using GroceryManager.Services.Users;
global using GroceryManager.Services.Supermarkets;
using GroceryManager.Services.Extensions;
using GroceryManager.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add configurations to project
builder.Services.AddGroceryConfigurations(builder.Configuration);

// Register DbContext conditionally
if (!builder.Environment.IsEnvironment("Test"))
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(connectionString,
            b => b.MigrationsAssembly("GroceryManager.Database.Migrations")));
}


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

public partial class Program { }