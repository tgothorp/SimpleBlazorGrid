using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.EntityFramework.Extensions;
using SimpleBlazorGrid.EntityFramework.Filters;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.DataSource
{
    public class SimpleGridEntityFrameworkDataSource<T> : ISimpleGridDataSource<T> where T : class
    {
        private IQueryable<T> _queryable;
        private readonly Expression<Func<T, object>> _primaryKey;
        private readonly DbContext _context;
        private EntityFrameworkFilterExpressionBuilder FilterExpressionBuilder { get; }

        public event EventHandler<IQueryable<T>> QueryExecuted;

        public SimpleGridEntityFrameworkDataSource(IQueryable<T> queryable)
        {
            _queryable = queryable;

            FilterExpressionBuilder = new EntityFrameworkFilterExpressionBuilder();
        }

        public SimpleGridEntityFrameworkDataSource(IQueryable<T> queryable, Expression<Func<T, object>> primaryKey, DbContext context)
        {
            _queryable = queryable;
            _primaryKey = primaryKey;
            _context = context;

            FilterExpressionBuilder = new EntityFrameworkFilterExpressionBuilder();
        }

        public async Task<TableState<T>> LoadItems(TableState<T> tableState, CancellationToken cancellationToken = default)
        {
            var query = _queryable;

            // Filter
            if (tableState.ActiveFilters.Any())
            {
                var filters = tableState.ActiveFilters
                    .Select(x => FilterExpressionBuilder.GetFilterExpression(x));

                var combined = filters.Aggregate((left, right) => left.And(right));
                if (combined != null)
                {
                    query = query.Where(combined);
                }
            }

            // Search + Sort
            query = ApplySearch(tableState.SearchQuery, tableState.SearchColumns, query);
            query = ApplySort(tableState.SortProperty, tableState.SortAscending, query);

            // Page
            var result = await LoadAsync(tableState.CurrentPage, tableState.ItemsPerPage, query, cancellationToken);
            
            tableState.SetItems(result.Items, result.TotalItemCount);
            return tableState;
        }

        public async Task<TableState<T>> UpdateItem(TableState<T> tableState, T item, CancellationToken cancellationToken = default)
        {
            // Load item from the database
            var parameter = Expression.Parameter(typeof(T), "x");
            var primaryKeyValue = Expression.Constant(_primaryKey.Compile().Invoke(item));
            var propertyAccess = ExpressionHelper.PropertyAccess<T>(ExpressionHelper.GetPropertyName(_primaryKey), parameter);
            
            var equals = Expression.Equal(propertyAccess, primaryKeyValue);
            var equalsFunc = Expression.Lambda<Func<T, bool>>(equals, parameter);

            var itemFromDatabase = await _queryable.Where(equalsFunc).SingleOrDefaultAsync(cancellationToken);
            if (itemFromDatabase is not null)
            {
                _context.Entry(itemFromDatabase);

                foreach (var editAction in tableState.ItemPropertiesToEdit)
                {
                    editAction.Apply(ref itemFromDatabase);
                }
                
                await _context.SaveChangesAsync(cancellationToken);
            }

            return tableState;
        }

        private IQueryable<T> ApplySearch(string searchQuery, HashSet<string> searchColumns, IQueryable<T> query)
        {
            if (searchQuery.IsNullOrEmpty() || !searchColumns.Any())
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var pattern = Expression.Constant($"%{searchQuery}%");
            var instance = Expression.Constant(EF.Functions, typeof(DbFunctions));
            var methodInfo = typeof(DbFunctionsExtensions).GetMethod("Like", new[] { typeof(DbFunctions), typeof(string), typeof(string) });

            var checks = new List<MethodCallExpression>();
            foreach (var column in searchColumns)
            {
                var property = ExpressionHelper.PropertyAccess<T>(column, parameter);

                var call = Expression.Call(
                    null,
                    methodInfo!,
                    instance,
                    property,
                    pattern);

                checks.Add(call);
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
                query = query.Where(lambda);
            }
            else
            {
                var check = checks.First();
                var lambda = Expression.Lambda<Func<T, bool>>(check, parameter);

                query = query.Where(lambda);
            }

            return query;
        }
        
        private IQueryable<T> ApplySort(string sortProperty, bool sortAscending, IQueryable<T> query)
        {
            if (sortProperty.IsNotNullOrEmpty())
            {
                var parameterExpression = Expression.Parameter(typeof(T), "obj");
                Expression propertyAccess = parameterExpression;

                foreach (var property in sortProperty.Split('.'))
                {
                    propertyAccess = Expression.Property(propertyAccess, property);
                }

                var lambdaExpression = Expression.Lambda(propertyAccess, parameterExpression);

                var orderByMethod = typeof(Queryable)
                    .GetMethods()
                    .Where(m => m.Name == (sortAscending ? "OrderBy" : "OrderByDescending"))
                    .Single(m => m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), propertyAccess.Type);

                query = (IQueryable<T>)orderByMethod.Invoke(null, new object[] { query, lambdaExpression });
            }

            return query;
        }

        private async Task<(T[] Items, int TotalItemCount)> LoadAsync(int currentPage, int itemsPerPage, IQueryable<T> query, CancellationToken cancellationToken)
        {
            var itemCount = await query.CountAsync(cancellationToken);

            query = query
                .Skip(currentPage * itemsPerPage)
                .Take(itemsPerPage);
            
            QueryExecuted?.Invoke(this, query);
            
            var items = await query
                .ToArrayAsync(cancellationToken);

            return (items, itemCount);
        }
    }
}