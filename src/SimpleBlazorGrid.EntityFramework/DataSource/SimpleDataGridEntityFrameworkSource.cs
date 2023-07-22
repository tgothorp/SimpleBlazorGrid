using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.Interfaces;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.EntityFramework.DataSource
{
    public class SimpleDataGridEntityFrameworkSource<T> : IDataGridSource<T>
    {
        private readonly IQueryable<T> _items;
        
        public FilterOptions FilterOptions { get; set; } = new();
        public SearchOptions SearchOptions { get; set; } = new();
        public SortOptions SortOptions { get; set; } = new();
        public PageOptions PageOptions { get; set; } = new();

        public SimpleDataGridEntityFrameworkSource(IQueryable<T> items)
        {
            _items = items;
        }
        
        public Task<T[]> Items(CancellationToken cancellationToken = default)
        {
            return _items
                .ToArrayAsync(cancellationToken);
        }
    }
}