using System;
using System.Linq.Expressions;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.EntityFramework.Filters;

public class EntityFrameworkFilterExpressionBuilder : FilterExpressionBuilder
{
    public override Expression<Func<T, bool>> GetFilterExpression<T>(Filter<T> filter)
    {
        return filter switch
        {
            SimpleStringFilter<T> stringFilter => StringFilterExpression(stringFilter),
            SimpleNumericFilter<T> numericFilter => NumericFilterExpression(numericFilter),
            SimpleNumericRangeFilter<T> numericRangeFilter => NumericRangeFilterExpression(numericRangeFilter),
            SimpleDateFilter<T> dateFilter => DateFilterExpression(dateFilter),
            SimpleDateRangeFilter<T> dateRangeFilter => DateRangeFilterExpression(dateRangeFilter),
            SimpleEnumFilter<T, Enum> simpleEnumFilter => EnumFilterExpression(simpleEnumFilter),
            _ => throw new ArgumentException($"There is no appropriate method to handle generation of a filter expression for the provided filter", nameof(filter))
        };
    }

    private Expression<Func<T, bool>> StringFilterExpression<T>(SimpleStringFilter<T> filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in filter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }
        
        var value = Expression.Constant(filter.Value, typeof(string));

        if (filter.IgnoreCase)
        {
            // string.Equals(string1, string2, StringComparison)
            var equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(string), typeof(StringComparison) });
            var equality = Expression.Call(null, equalsMethod, propertyAccess, value, Expression.Constant(StringComparison.OrdinalIgnoreCase));

            return Expression.Lambda<Func<T, bool>>(equality, parameter);
        }
        else
        {
            var equality = Expression.Equal(propertyAccess, value);
            return Expression.Lambda<Func<T, bool>>(equality, parameter);
        }
    }

    private Expression<Func<T, bool>> NumericFilterExpression<T>(SimpleNumericFilter<T> numericFilter)
    {
        throw new NotImplementedException();
    }

    private Expression<Func<T, bool>> NumericRangeFilterExpression<T>(SimpleNumericRangeFilter<T> numericRangeFilter)
    {
        throw new NotImplementedException();
    }

    private Expression<Func<T,bool>> DateFilterExpression<T>(SimpleDateFilter<T> dateFilter)
    {
        throw new NotImplementedException();
    }

    private Expression<Func<T, bool>> DateRangeFilterExpression<T>(SimpleDateRangeFilter<T> dateRangeFilter)
    {
        throw new NotImplementedException();
    }

    private Expression<Func<T, bool>> EnumFilterExpression<T>(SimpleEnumFilter<T, Enum> simpleEnumFilter)
    {
        throw new NotImplementedException();
    }
}