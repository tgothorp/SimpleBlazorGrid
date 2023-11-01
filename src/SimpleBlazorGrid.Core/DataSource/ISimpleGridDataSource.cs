using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlazorGrid.DataSource
{
    public interface ISimpleGridDataSource<T>
    {
        public Task<TableState<T>> LoadItems(TableState<T> tableState, CancellationToken cancellationToken = default);
    }
}