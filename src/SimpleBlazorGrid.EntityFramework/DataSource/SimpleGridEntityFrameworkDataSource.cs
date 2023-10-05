using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.DataSource;
using SimpleBlazorGrid.EntityFramework.Extensions;
using SimpleBlazorGrid.EntityFramework.Filters;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Filters;
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

            // Sort
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
                
                query = (IQueryable<T>) orderByMethod.Invoke(null, new object[] { query, lambdaExpression });
            }

            // Page
            PageOptions.TotalItemCount = await query.CountAsync(cancellationToken);
            PageOptions.MaxPage = (int)Math.Ceiling((decimal)PageOptions.TotalItemCount / PageOptions.ItemsPerPage);

            return await query
                .Skip(PageOptions.CurrentPage * PageOptions.ItemsPerPage)
                .Take(PageOptions.ItemsPerPage)
                .ToArrayAsync(cancellationToken);
        }
    }
}