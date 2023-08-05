using System.Threading;
using System.Threading.Tasks;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IDataGridSource<T>
    {
        public FilterOptions FilterOptions { get; set; }
        public SearchOptions SearchOptions { get; set; }
        public SortOptions SortOptions { get; set; }
        public PageOptions PageOptions { get; set; }

        public Task<T[]> Items(CancellationToken cancellationToken = default);
    }
}