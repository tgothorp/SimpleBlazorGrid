using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleBlazorGrid.Filters;

public class EnumerableFilterExpressionBuilder : FilterExpressionBuilder
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
            EnumFilter<T> simpleEnumFilter => EnumFilterExpression(simpleEnumFilter),
            _ => throw new ArgumentException($"There is no appropriate method to create a filter expression for the provided filter", nameof(filter))
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

    private Expression<Func<T, bool>> NumericFilterExpression<T>(SimpleNumericFilter<T> filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in filter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }

        var propertyType = propertyAccess.Type;
        var value = Expression.Constant(Convert.ChangeType(filter.Value, propertyType), propertyType);

        var equality = Expression.Equal(propertyAccess, value);
        return Expression.Lambda<Func<T, bool>>(equality, parameter);
    }

    private Expression<Func<T, bool>> NumericRangeFilterExpression<T>(SimpleNumericRangeFilter<T> filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in filter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }

        var propertyType = propertyAccess.Type;
        var lowValue = Expression.Constant(Convert.ChangeType(filter.LowValue, propertyType), propertyType);
        var highValue = Expression.Constant(Convert.ChangeType(filter.HighValue, propertyType), propertyType);

        // x => x.Property >= lowValue && x.Property <= highValue
        var greaterThanLowValue = Expression.GreaterThanOrEqual(propertyAccess, lowValue);
        var lessThanHighValue = Expression.LessThanOrEqual(propertyAccess, highValue);
        var and = Expression.And(greaterThanLowValue, lessThanHighValue);

        return Expression.Lambda<Func<T, bool>>(and, parameter);
    }

    private Expression<Func<T, bool>> DateFilterExpression<T>(SimpleDateFilter<T> filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in filter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }

        var propertyType = propertyAccess.Type;
        if (propertyType == typeof(DateTime?))
            propertyAccess = Expression.Property(propertyAccess, "Value");

        if (filter.IncludeTime)
        {
            var value = Expression.Constant(filter.Value.Value, typeof(DateTime));
            var equal = Expression.Equal(propertyAccess, value);

            return Expression.Lambda<Func<T, bool>>(equal, parameter);
        }
        else
        {
            var value = Expression.Constant(filter.Value.Value.Date, typeof(DateTime));
            var equal = Expression.Equal(propertyAccess, value);

            return Expression.Lambda<Func<T, bool>>(equal, parameter);
        }
    }

    private Expression<Func<T, bool>> DateRangeFilterExpression<T>(SimpleDateRangeFilter<T> filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in filter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }

        var propertyType = propertyAccess.Type;
        if (propertyType == typeof(DateTime?))
            propertyAccess = Expression.Property(propertyAccess, "Value");

        if (!filter.IncludeTime)
            propertyAccess = Expression.Property(propertyAccess, "Date");

        var low = Expression.Constant(filter.LowValue.Value, typeof(DateTime));
        var high = Expression.Constant(filter.HighValue.Value, typeof(DateTime));

        var greaterThanLow = filter.Inclusive
            ? Expression.GreaterThanOrEqual(propertyAccess, low)
            : Expression.GreaterThan(propertyAccess, low);

        var lessThanHigh = filter.Inclusive
            ? Expression.LessThanOrEqual(propertyAccess, high)
            : Expression.LessThan(propertyAccess, high);

        var and = Expression.And(greaterThanLow, lessThanHigh);

        return Expression.Lambda<Func<T, bool>>(and, parameter);
    }

    private Expression<Func<T, bool>> EnumFilterExpression<T>(EnumFilter<T> filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in filter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }

        if (filter.AllowMultiple)
        {
            Expression propertyToString = Expression.Call(Expression.Convert(propertyAccess, typeof(object)), typeof(object).GetMethod("ToString"));

            var containsMethodCall = Expression.Call(Expression.Constant(filter.SelectedValues), typeof(List<string>).GetMethod("Contains"), propertyToString);
            return Expression.Lambda<Func<T, bool>>(containsMethodCall, parameter);
        }

        var convertedValue = Enum.Parse(filter.EnumType, filter.SelectedValues[0], true);
        var value = Expression.Constant(convertedValue, filter.EnumType);
        var equal = Expression.Equal(propertyAccess, value);

        return Expression.Lambda<Func<T, bool>>(equal, parameter);
    }
}