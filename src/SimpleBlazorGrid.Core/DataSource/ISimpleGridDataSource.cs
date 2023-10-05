using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SimpleBlazorGrid.Filters;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.DataSource
{
    public interface ISimpleGridDataSource<T>
    {
        public SortOptions SortOptions { get; set; }
        public PageOptions PageOptions { get; set; }
        public SearchOptions SearchOptions { get; set; }
        public IEnumerable<Filter<T>> Filters { get; set; }

        public Task<T[]> Items(CancellationToken cancellationToken = default);
    }
}