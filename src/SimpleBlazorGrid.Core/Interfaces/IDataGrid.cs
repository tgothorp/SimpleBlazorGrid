using System.Threading.Tasks;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IDataGrid<T>
    {
        Task AddColumn(IColumn<T> column);

        Task AddSimpleFilter(Filter<T> filter);
        Task EditSimpleFilter(Filter<T> filter);

        Task ReloadData();
        
        Task Sort(string property, bool sortAscending);
    }
}