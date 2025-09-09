using Microsoft.EntityFrameworkCore;
using GroceryManager.Database.Entities;

namespace GroceryManager.Database;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options) { }

  public DbSet<User> Users { get; set; }
  public DbSet<ShoppingList> ShoppingLists { get; set; }
  public DbSet<Item> Items { get; set; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // User
    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(u => u.Id);
      entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
      entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
      entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
    });

    base.OnModelCreating(modelBuilder);
  }
}
