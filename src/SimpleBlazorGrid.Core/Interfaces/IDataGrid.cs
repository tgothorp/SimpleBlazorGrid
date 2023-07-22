using System.Threading.Tasks;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IDataGrid<T>
    {
        Task AddColumn(IColumn<T> column);
        Task AddFilter(IFilter<T> filter);
        Task Sort(string property, bool sortAscending);
    }
}