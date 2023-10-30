using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleBlazorGrid.EntityFramework.Extensions;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Filters;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.EntityFramework.Filters;

public class EntityFrameworkFilterExpressionBuilder
{
    private EnumerableFilterExpressionBuilder _enumerableFilterExpression = new();
    
    public Expression<Func<T, bool>> GetFilterExpression<T>(Filter<T> filter)
    {
        return filter switch
        {
            SimpleStringFilter<T> stringFilter => StringFilterExpression(stringFilter),
            SimpleNumericFilter<T> numericFilter => _enumerableFilterExpression.NumericFilterExpression(numericFilter),
            SimpleNumericRangeFilter<T> numericRangeFilter => _enumerableFilterExpression.NumericRangeFilterExpression(numericRangeFilter),
            SimpleDateFilter<T> dateFilter => _enumerableFilterExpression.DateFilterExpression(dateFilter),
            SimpleDateRangeFilter<T> dateRangeFilter => _enumerableFilterExpression.DateRangeFilterExpression(dateRangeFilter),
            SimpleEnumFilter<T, Enum> simpleEnumFilter => _enumerableFilterExpression.EnumFilterExpression(simpleEnumFilter),
            _ => throw new ArgumentException($"There is no appropriate method to handle generation of a filter expression for the provided filter", nameof(filter))
        };
    }

    /// <summary>
    /// Entity framework specific implementation, the enumerable string filter uses string.Equals(arg1, arg2, StringComparison) which
    /// cannot be translated by entity framework so we use .ToLower() instead which does the job despite being poor performance-wise
    /// </summary>
    private Expression<Func<T, bool>> StringFilterExpression<T>(SimpleStringFilter<T> stringFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in stringFilter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }

        if (!stringFilter.Exact)
        {
            var pattern = Expression.Constant($"%{stringFilter.Value}%");
            var instance = Expression.Constant(EF.Functions, typeof(DbFunctions));
            var methodInfo = typeof(DbFunctionsExtensions).GetMethod("Like", new[] { typeof(DbFunctions), typeof(string), typeof(string) });
            
            var like = Expression.Call(null, methodInfo!, instance, propertyAccess, pattern);
            return Expression.Lambda<Func<T, bool>>(like, parameter);
        }

        if (stringFilter.IgnoreCase)
        {
            // Call .ToLower on the target property and filter value;
            var value = Expression.Constant(stringFilter.Value.ToLower(), typeof(string));
            var toLower = Expression.Call(propertyAccess, typeof(string).GetMethod("ToLower", Type.EmptyTypes));

            var equality = Expression.Equal(toLower, value);
            return Expression.Lambda<Func<T, bool>>(equality, parameter);
        }
        else
        {
            var value = Expression.Constant(stringFilter.Value, typeof(string));
            var equality = Expression.Equal(propertyAccess, value);
            return Expression.Lambda<Func<T, bool>>(equality, parameter);
        }
    }
}