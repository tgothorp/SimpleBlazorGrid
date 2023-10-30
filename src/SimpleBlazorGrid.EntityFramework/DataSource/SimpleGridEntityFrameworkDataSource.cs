using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.DataSource;
using SimpleBlazorGrid.EntityFramework.Extensions;
using SimpleBlazorGrid.EntityFramework.Filters;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Filters;
using SimpleBlazorGrid.Helpers;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.EntityFramework.DataSource
{
    public class SimpleGridEntityFrameworkDataSource<T> : ISimpleGridDataSource<T> where T : class
    {
        private IQueryable<T> _queryable;

        public SortOptions SortOptions { get; set; } = new();
        public PageOptions PageOptions { get; set; } = new();
        public SearchOptions SearchOptions { get; set; } = new();
        public IEnumerable<Filter<T>> Filters { get; set; }
        public EntityFrameworkFilterExpressionBuilder FilterExpressionBuilder { get; }

        public SimpleGridEntityFrameworkDataSource(IQueryable<T> queryable)
        {
            _queryable = queryable;
            FilterExpressionBuilder = new EntityFrameworkFilterExpressionBuilder();
        }


        public async Task<T[]> Items(CancellationToken cancellationToken = default)
        {
            var query = _queryable;

            // Filter
            if (Filters.Any())
            {
                var filters = Filters
                    .Select(x => FilterExpressionBuilder.GetFilterExpression(x));

                var combined = filters.Aggregate((left, right) => left.And(right));
                if (combined != null)
                {
                    query = query.Where(combined);
                }
            }

            // Search + Sort
            query = ApplySearch(query);
            query = ApplySort(query);

            // Page
            PageOptions.TotalItemCount = await query.CountAsync(cancellationToken);
            PageOptions.MaxPage = (int)Math.Ceiling((decimal)PageOptions.TotalItemCount / PageOptions.ItemsPerPage);

            return await query
                .Skip(PageOptions.CurrentPage * PageOptions.ItemsPerPage)
                .Take(PageOptions.ItemsPerPage)
                .ToArrayAsync(cancellationToken);
        }

        private IQueryable<T> ApplySearch(IQueryable<T> query)
        {
            if (SearchOptions.Query.IsNullOrEmpty() || !SearchOptions.Columns.Any())
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var pattern = Expression.Constant($"%{SearchOptions.Query}%");
            var instance = Expression.Constant(EF.Functions, typeof(DbFunctions));
            var methodInfo = typeof(DbFunctionsExtensions).GetMethod("Like", new[] { typeof(DbFunctions), typeof(string), typeof(string) });

            var checks = new List<MethodCallExpression>();
            foreach (var column in SearchOptions.Columns)
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
        
        private IQueryable<T> ApplySort(IQueryable<T> query)
        {
            if (SortOptions.Property.IsNotNullOrEmpty())
            {
                var parameterExpression = Expression.Parameter(typeof(T), "obj");
                Expression propertyAccess = parameterExpression;

                foreach (var property in SortOptions.Property.Split('.'))
                {
                    propertyAccess = Expression.Property(propertyAccess, property);
                }

                var lambdaExpression = Expression.Lambda(propertyAccess, parameterExpression);

                var orderByMethod = typeof(Queryable)
                    .GetMethods()
                    .Where(m => m.Name == (SortOptions.Ascending ? "OrderBy" : "OrderByDescending"))
                    .Single(m => m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), propertyAccess.Type);

                query = (IQueryable<T>)orderByMethod.Invoke(null, new object[] { query, lambdaExpression });
            }

            return query;
        }
    }
}