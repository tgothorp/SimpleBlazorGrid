using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlazorGrid.DataSource
{
    public interface ISimpleGridDataSource<T>
    {
        public Task LoadItems(ref TableState<T> tableState, CancellationToken cancellationToken = default);
    }
}