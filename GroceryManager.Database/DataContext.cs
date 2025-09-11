using Microsoft.EntityFrameworkCore;
using GroceryManager.Database.Entities;

namespace GroceryManager.Database;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options) { }

  public DbSet<User> Users { get; set; }
  public DbSet<ShoppingList> ShoppingLists { get; set; }
  public DbSet<Item> Items { get; set; }
  public DbSet<Supermarket> Supermarkets { get; set; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Supermarket>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Supermarket);

    base.OnModelCreating(modelBuilder);
  }
}
