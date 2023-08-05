using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SimpleBlazorGrid.Interfaces;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.DataSource
{
    public class SimpleDataGridEnumerableSource<T> : IDataGridSource<T>
    {
        private IEnumerable<T> Source { get; }

        public FilterOptions FilterOptions { get; set; } = new();
        public SearchOptions SearchOptions { get; set; } = new();
        public SortOptions SortOptions { get; set; } = new();
        public PageOptions PageOptions { get; set; } = new();

        public SimpleDataGridEnumerableSource(IEnumerable<T> source)
        {
            Source = source;
        }
        public Task<T[]> Items(CancellationToken cancellationToken = default)
        {
            var items = Source;
            items = ApplyFiltering(items);
            items = ApplySearch(items);
            items = ApplySorting(items);
            items = ApplyPaging(items);

            return Task.FromResult(items.ToArray());
        }

        private IEnumerable<T> ApplyFiltering(IEnumerable<T> items)
        {
            if (FilterOptions.Options.Any())
            {
                var filters = FilterOptions.Options
                    .Select(x => x.Filter<T>())
                    .Where(x => x is not null);

                items = filters.Aggregate(items, (current, filter) => current.Where(filter.Compile()));
            }

            return items;
        }

        private IEnumerable<T> ApplySearch(IEnumerable<T> items)
        {
            // TODO
            return items;
        }

        private IEnumerable<T> ApplySorting(IEnumerable<T> items)
        {
            object GetValueByPropertyChain(object obj, string[] propertyNames)
            {
                foreach (var name in propertyNames)
                {
                    var property = obj.GetType().GetProperty(name);
                    obj = property.GetValue(obj);
                }

                return obj;
            }

            if (!string.IsNullOrEmpty(SortOptions.Property))
            {
                string[] propertyNames = SortOptions.Property.Split('.');
                Type targetType = typeof(T);
                PropertyInfo property = null;

                foreach (var name in propertyNames)
                {
                    property = targetType.GetProperty(name);

                    if (property == null)
                    {
                        // It looks like the property name provided was nowhere on the object
                        return items;
                    }

                    targetType = property.PropertyType;
                }

                return SortOptions.Ascending 
                    ? items.OrderBy(x => GetValueByPropertyChain(x, propertyNames)) 
                    : items.OrderByDescending(x => GetValueByPropertyChain(x, propertyNames));
            }

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