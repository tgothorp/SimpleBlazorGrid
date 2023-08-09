using System.Threading.Tasks;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IDataGrid<T>
    {
        Task AddColumn(IColumn<T> column);
        Task AddFilter(IFilter<T> filter);
        
        Task AddSimpleFilter(Filter<T> filter);
        Task EditSimpleFilter(Filter<T> filter);
        Task ApplySimpleFilter(Filter<T> filter);
        Task RemoveSimpleFilter(Filter<T> filter);
        
        Task Sort(string property, bool sortAscending);
    }
}