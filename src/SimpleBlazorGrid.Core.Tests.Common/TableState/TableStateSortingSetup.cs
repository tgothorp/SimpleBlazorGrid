using SimpleBlazorGrid.Core.Tests.Common.Extensions;

namespace SimpleBlazorGrid.Core.Tests.Common.TableState;

public class TableStateSortingSetup<T>
{
    private readonly TableStateSetup<T> _setup;

    public TableStateSortingSetup(TableStateSetup<T> setup)
    {
        _setup = setup;
    }
    
    public Task<TableStateSetup<T>> SortByProperty(string propertyName, bool ascending)
    {
        _setup.TableState.SetProperty(x => x.SortProperty, propertyName);
        _setup.TableState.SetProperty(x => x.SortAscending, ascending);
        
        return Task.FromResult(_setup);
    }

    public Task<TableStateSetup<T>> SortByPropertyAscending(string propertyName)
    {
        _setup.TableState.SetProperty(x => x.SortProperty, propertyName);
        _setup.TableState.SetProperty(x => x.SortAscending, true);
        
        return Task.FromResult(_setup);
    }
    
    public Task<TableStateSetup<T>> SortByPropertyDescending(string propertyName)
    {
        _setup.TableState.SetProperty(x => x.SortProperty, propertyName);
        _setup.TableState.SetProperty(x => x.SortAscending, false);
        
        return Task.FromResult(_setup);
    }
}