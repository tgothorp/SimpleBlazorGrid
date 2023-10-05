using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Filters;
using SimpleBlazorGrid.Helpers;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.DataSource
{
    public class SimpleGridEnumerableDataSource<T> : ISimpleGridDataSource<T>
    {
        private IEnumerable<T> Source { get; }
        private EnumerableFilterExpressionBuilder FilterExpressionBuilder { get; }
        public IEnumerable<Filter<T>> Filters { get; set; }
        public SortOptions SortOptions { get; set; } = new();
        public PageOptions PageOptions { get; set; } = new();
        public SearchOptions SearchOptions { get; set; } = new();

        public SimpleGridEnumerableDataSource(IEnumerable<T> source)
        {
            Source = source;
            FilterExpressionBuilder = new EnumerableFilterExpressionBuilder();
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
            if (Filters.Any())
            {
                var filters = Filters
                    .Select(x => FilterExpressionBuilder.GetFilterExpression(x));

                items = filters.Aggregate(items, (current, filter) => current.Where(filter.Compile()));
            }

            return items;
        }

        private IEnumerable<T> ApplySearch(IEnumerable<T> items)
        {
            if (SearchOptions.Query.IsNullOrEmpty() || SearchOptions.Columns.Count == 0)
                return items;

            var parameter = Expression.Parameter(typeof(T), "x");
            var query = Expression.Constant(SearchOptions.Query, typeof(string));
            var comparison = Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison));

            var checks = new List<ConditionalExpression>();
            foreach (var column in SearchOptions.Columns)
            {
                var property = ExpressionHelper.PropertyAccess<T>(column, parameter);
                var propertyIsNotNull = Expression.NotEqual(property, Expression.Constant(null));

                var contains = Expression.Condition(
                    propertyIsNotNull,
                    Expression.Call(property,
                        typeof(string).GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) })!,
                        query,
                        comparison),
                    Expression.Constant(false));

                checks.Add(contains);
            }

            if (checks.Count > 1)
            {
                BinaryExpression or = null;

                for (int i = 1; i < checks.Count; i++)
                {
                    or = or is null 
                        ? Expression.OrElse(checks[i - 1], checks[i]) 
                        : Expression.OrElse(or, checks[i]);
                }
                
                var lambda = Expression.Lambda<Func<T, bool>>(or, parameter);
                items = items.Where(lambda.Compile());
            }
            else
            {
                var check = checks.First();
                var lambda = Expression.Lambda<Func<T, bool>>(check, parameter);

                items = items.Where(lambda.Compile());
            }
            
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