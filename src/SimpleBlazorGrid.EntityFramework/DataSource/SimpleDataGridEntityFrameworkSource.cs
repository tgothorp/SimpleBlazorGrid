using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.EntityFramework.Extensions;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Interfaces;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.EntityFramework.DataSource
{
    public class SimpleDataGridEntityFrameworkSource<T> : IDataGridSource<T> where T : class
    {
        private DbSet<T> _dbSet;

        public FilterOptions FilterOptions { get; set; } = new();
        public SearchOptions SearchOptions { get; set; } = new();
        public SortOptions SortOptions { get; set; } = new();
        public PageOptions PageOptions { get; set; } = new();

        public SimpleDataGridEntityFrameworkSource(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public async Task<T[]> Items(CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();

            // Filter
            if (FilterOptions.Options.Any())
            {
                var filters = FilterOptions.Options.Select(x => x.Filter<T>());
                var combined = filters.Aggregate((left, right) => left.And(right));

                if (combined != null)
                {
                    query = query.Where(combined);
                }
            }

            // Sort
            if (SortOptions.Property.IsNotNullOrEmpty())
            {
                var parameterExpression = Expression.Parameter(typeof(T), "x");
                var propertyInfo = typeof(T).GetProperty(SortOptions.Property);
                var propertyAccessExpression = Expression.Property(parameterExpression, propertyInfo);

                var lambdaExpression = Expression.Lambda(propertyAccessExpression, parameterExpression);
                var orderByMethod = typeof(Queryable)
                    .GetMethods()
                    .Where(m => m.Name == (SortOptions.Ascending ? "OrderBy" : "OrderByDescending"))
                    .Single(m => m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), propertyInfo.PropertyType);
                
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