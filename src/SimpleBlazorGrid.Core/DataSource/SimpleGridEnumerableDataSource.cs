using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Filters;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.DataSource
{
    public class SimpleGridEnumerableDataSource<T> : ISimpleGridDataSource<T>
    {
        private IEnumerable<T> Source { get; set; }
        private EnumerableFilterExpressionBuilder FilterExpressionBuilder { get; }

        public SimpleGridEnumerableDataSource(IEnumerable<T> source)
        {
            Source = source;
            FilterExpressionBuilder = new EnumerableFilterExpressionBuilder();
        }

        public Task<TableState<T>> LoadItems(TableState<T> tableState, CancellationToken cancellationToken = default)
        {
            var items = Source;

            items = ApplyFiltering(tableState.ActiveFilters, items);
            items = ApplySearch(tableState.SearchQuery, tableState.SearchColumns, items);
            items = ApplySorting(tableState.SortProperty, tableState.SortAscending, items);

            var totalItemCount = items.Count();
            var array = ApplyPaging2(tableState, items);

            tableState.SetItems(array, totalItemCount);

            return Task.FromResult(tableState);
        }

        public Task<TableState<T>> UpdateItem(TableState<T> tableState, T item, CancellationToken cancellationToken = default)
        {
            var items = Source.ToList();
            items.Remove(item);

            foreach (var editAction in tableState.ItemPropertiesToEdit)
            {
                editAction.Apply(ref item);
            }
            
            items.Add(item);
            Source = items;

            tableState.ClearEditActions();
            return Task.FromResult(tableState);
        }

        private IEnumerable<T> ApplyFiltering(List<Filter<T>> activeFilters, IEnumerable<T> items)
        {
            if (activeFilters is not null && activeFilters.Any())
            {
                var filters = activeFilters
                    .Select(x => FilterExpressionBuilder.GetFilterExpression(x));

                items = filters.Aggregate(items, (current, filter) => current.Where(filter.Compile()));
            }

            return items;
        }

        private IEnumerable<T> ApplySearch(string searchQuery, HashSet<string> searchColumns, IEnumerable<T> items)
        {
            if (searchQuery.IsNullOrEmpty() || searchColumns.Count == 0)
                return items;

            var parameter = Expression.Parameter(typeof(T), "x");
            var query = Expression.Constant(searchQuery, typeof(string));
            var comparison = Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison));

            var checks = new List<ConditionalExpression>();
            foreach (var column in searchColumns)
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

        private IEnumerable<T> ApplySorting(string sortProperty, bool sortAscending, IEnumerable<T> items)
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

            if (sortProperty.IsNotNullOrEmpty())
            {
                string[] propertyNames = sortProperty.Split('.');
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

                return sortAscending 
                    ? items.OrderBy(x => GetValueByPropertyChain(x, propertyNames)) 
                    : items.OrderByDescending(x => GetValueByPropertyChain(x, propertyNames));
            }

            return items;
        }

        private T[] ApplyPaging(TableState<T> tableState, IEnumerable<T> items)
        {
            var array = items.ToArray();
            
            tableState.UpdatePagingValues(array.Length);
            
            array = array
                .Skip(tableState.CurrentPage * tableState.ItemsPerPage)
                .Take(tableState.ItemsPerPage)
                .ToArray();

            return array;
        }
        
        private T[] ApplyPaging2(TableState<T> tableState, IEnumerable<T> items)
        {
            return items
                .Skip(tableState.CurrentPage * tableState.ItemsPerPage)
                .Take(tableState.ItemsPerPage)
                .ToArray();
        }
    }
}