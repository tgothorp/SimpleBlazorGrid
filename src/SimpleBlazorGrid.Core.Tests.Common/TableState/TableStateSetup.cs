using SimpleBlazorGrid.DataSource;

namespace SimpleBlazorGrid.Core.Tests.Common.TableState;

public class TableStateSetup<T>
{
    public TableState<T> TableState { get; private set; }
    public ISimpleGridDataSource<T> GridDataSource { get; private set; }

    public TableStateSearchingSetup<T> Searching { get; }
    public TableStateSortingSetup<T> Sorting { get; set; }

    public TableStateSetup()
    {
        Searching = new TableStateSearchingSetup<T>(this);
        Sorting = new TableStateSortingSetup<T>(this);
    }

    public Task<TableStateSetup<T>> CreateNewTableState(int itemsPerPage = 5)
    {
        TableState = new TableState<T>(itemsPerPage);
        return Task.FromResult(this);
    }

    public Task<TableStateSetup<T>> WithDataSource(ISimpleGridDataSource<T> dataSource)
    {
        GridDataSource = dataSource;
        return Task.FromResult(this);
    }

    public Task<TableStateSetup<T>> WithEnumerableDataSource(IEnumerable<T> items)
    {
        GridDataSource = new SimpleGridEnumerableDataSource<T>(items);
        return Task.FromResult(this);
    }

    public async Task<TableStateSetup<T>> ReloadData()
    {
        TableState = await GridDataSource.LoadItems(TableState, CancellationToken.None);
        return this;
    }
}