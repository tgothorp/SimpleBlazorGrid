using SimpleBlazorGrid.EntityFramework.DataSource;
using SimpleBlazorGrid.Filters;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.EntityFramework.Tests.Setup;

public static class TestSetup
{
    public static SimpleGridEntityFrameworkDataSource<T> CreateDataSource<T>(IQueryable<T> table, params Filter<T>[] filters) where T : class
    {
        return new SimpleGridEntityFrameworkDataSource<T>(table)
        {
            Filters = filters,
            PageOptions = new PageOptions { ItemsPerPage = 50 },
            SortOptions = new SortOptions()
        };
    }
}