using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.Interfaces
{
    public class DataGridEnumerableSource<T> : IDataGridSource<T>
    {
        private IEnumerable<T> Source { get; }

        public FilterOptions FilterOptions { get; set; } = new();
        public SearchOptions SearchOptions { get; set; } = new();
        public PageOptions PageOptions { get; set; } = new();

        public DataGridEnumerableSource(IEnumerable<T> source)
        {
            Source = source;
        }


        public T[] Items()
        {
            // TODO, filtering, sorting etc.

            var items = Source;
            items = ApplyFiltering(items);
            items = ApplySearch(items);
            items = ApplyPaging(items);

            return items.ToArray();
        }

        private IEnumerable<T> ApplyFiltering(IEnumerable<T> items)
        {
            // TODO
            return items;
        }

        private IEnumerable<T> ApplySearch(IEnumerable<T> items)
        {
            // TODO
            return items;
        }

        private IEnumerable<T> ApplyPaging(IEnumerable<T> items)
        {
            PageOptions.TotalItemCount = items.Count();
            PageOptions.MaxPage = (int)Math.Ceiling((decimal)PageOptions.TotalItemCount / PageOptions.ItemsPerPage); 
                
            return items
                .Skip(PageOptions.CurrentPage * PageOptions.ItemsPerPage)
                .Take(PageOptions.ItemsPerPage);
        }
    }
}