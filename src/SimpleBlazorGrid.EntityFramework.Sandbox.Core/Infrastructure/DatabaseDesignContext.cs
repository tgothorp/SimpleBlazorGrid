using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;

public class DatabaseDesignContext : IDesignTimeDbContextFactory<DatabaseContext>
{
    /// <summary>
    /// Used by the dotnet ef tool to create a db context so that migrations can be applied
    /// </summary>
    public DatabaseContext CreateDbContext(string[] args)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var databaseName = "simpleblazorgrid.db";

        var connectionString = $"Data Source={Path.Join(path, databaseName)}";

        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(connectionString);

        return new DatabaseContext(options.Options);
    }
}