using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;

namespace SimpleBlazorGrid.EntityFramework.Sandbox;

public static class DatabaseContextFactory
{
    private static string? ConnectionString { get; set; }

    public static DatabaseContext Create()
    {
        if (ConnectionString is null)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var databaseName = "simpleblazorgrid.db";

            ConnectionString = $"Data Source={Path.Join(path, databaseName)}";
        }

        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(ConnectionString)
            .Options;

        return new DatabaseContext(options);
    }
}