using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;

namespace SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;

public class DatabaseContext : DbContext
{
    public string DbPath { get; }

    public DatabaseContext(bool testDatabase = false) // TODO, This is only for testing, but it's still pretty bad (but it works).
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var databaseName = testDatabase ? "simpleblazorgrid_test.db" : "simpleblazorgrid.db";
        
        DbPath = Path.Join(path, databaseName);
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
        base.OnConfiguring(optionsBuilder);
    }
}