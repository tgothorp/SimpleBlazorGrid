using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;

namespace SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;

public class DatabaseContext : DbContext
{
    public string? DbPath { get; } = null;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var databaseName = "simpleblazorgrid.db";
        
        DbPath = Path.Join(path, databaseName);
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}