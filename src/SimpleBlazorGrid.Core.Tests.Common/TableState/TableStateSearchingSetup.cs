using SimpleBlazorGrid.Core.Tests.Common.Extensions;

namespace SimpleBlazorGrid.Core.Tests.Common.TableState;

public class TableStateSearchingSetup<T>
{
    private readonly TableStateSetup<T> _setup;

    public TableStateSearchingSetup(TableStateSetup<T> setup)
    {
        _setup = setup;
    }

    public Task<TableStateSetup<T>> WithSearchableColumns(params string[] searchableColumns)
    {
        var columns = searchableColumns.ToHashSet();
        _setup.TableState.SetProperty(x => x.SearchColumns, columns);

        return Task.FromResult(_setup);
    }
    
    public Task<TableStateSetup<T>> WithSearchQuery(string query)
    {
        _setup.TableState.SetProperty(x => x.SearchQuery, query);

        return Task.FromResult(_setup);
    }
}