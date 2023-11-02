using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlazorGrid.DataSource
{
    public interface ISimpleGridDataSource<T>
    {
        public Task<TableState<T>> LoadItems(TableState<T> tableState, CancellationToken cancellationToken = default);
        public Task<TableState<T>> UpdateItem(TableState<T> tableState, T item, CancellationToken cancellationToken = default);
    }
}