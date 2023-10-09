using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.Filters;

public class EnumerableFilterExpressionBuilder
{
    public Expression<Func<T, bool>> GetFilterExpression<T>(Filter<T> filter)
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

    public Expression<Func<T, bool>> StringFilterExpression<T>(SimpleStringFilter<T> stringFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = ExpressionHelper.PropertyAccess(stringFilter, parameter);

        var value = Expression.Constant(stringFilter.Value, typeof(string));

        if (!stringFilter.Exact)
        {
            var comparison = Expression.Constant(stringFilter.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal, typeof(StringComparison));
            var propertyIsNotNull = Expression.NotEqual(propertyAccess, Expression.Constant(null));

            var contains = Expression.Condition(
                propertyIsNotNull,
                Expression.Call(propertyAccess,
                    typeof(string).GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) })!,
                    value,
                    comparison),
                Expression.Constant(false));

            // x => x.Property.Contains(FilterValue, StringComparison)
            return Expression.Lambda<Func<T, bool>>(contains, parameter);
        }

        if (stringFilter.IgnoreCase)
        {
            var equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(string), typeof(StringComparison) });
            var equality = Expression.Call(null, equalsMethod, propertyAccess, value, Expression.Constant(StringComparison.OrdinalIgnoreCase));

            // x => string.Equals(x.Property, FilterValue, StringComparison.OrdinalIgnoreCase)
            return Expression.Lambda<Func<T, bool>>(equality, parameter);
        }
        else
        {
            var equality = Expression.Equal(propertyAccess, value);
            
            // x => x.Property == FilterValue
            return Expression.Lambda<Func<T, bool>>(equality, parameter);
        }
    }

    public Expression<Func<T, bool>> NumericFilterExpression<T>(SimpleNumericFilter<T> numericFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = ExpressionHelper.PropertyAccess(numericFilter, parameter);

        var value = Expression.Constant(NumericHelper.ConvertStringToNumericType(numericFilter.Value, propertyAccess.Type), propertyAccess.Type);
        var equality = Expression.Equal(propertyAccess, value);
        
        // x => x.Property == FilterValue
        return Expression.Lambda<Func<T, bool>>(equality, parameter);
    }

    public Expression<Func<T, bool>> NumericRangeFilterExpression<T>(SimpleNumericRangeFilter<T> numericRangeFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = ExpressionHelper.PropertyAccess(numericRangeFilter, parameter);
        var propertyType = propertyAccess.Type;

        var lowValue = Expression.Constant(NumericHelper.ConvertStringToNumericType(numericRangeFilter.LowValue, propertyType), propertyType);
        var highValue = Expression.Constant(NumericHelper.ConvertStringToNumericType(numericRangeFilter.HighValue, propertyType), propertyType);

        var greaterThanLowValue = Expression.GreaterThanOrEqual(propertyAccess, lowValue);
        var lessThanHighValue = Expression.LessThanOrEqual(propertyAccess, highValue);
        var and = Expression.And(greaterThanLowValue, lessThanHighValue);

        // x => x.Property >= lowValue && x.Property <= highValue
        return Expression.Lambda<Func<T, bool>>(and, parameter);
    }

    public Expression<Func<T, bool>> DateFilterExpression<T>(SimpleDateFilter<T> dateFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = ExpressionHelper.PropertyAccess(dateFilter, parameter);
        var propertyType = propertyAccess.Type;

        if (dateFilter.IncludeTime || (Nullable.GetUnderlyingType(propertyType) ?? propertyType) == typeof(DateOnly))
        {
            var value = Expression.Constant(DateTimeHelper.ConvertDateTimeToTargetType(propertyType, dateFilter.Value), propertyType);
            var equal = Expression.Equal(propertyAccess, value);

            // x => x.Property == FilterValue
            return Expression.Lambda<Func<T, bool>>(equal, parameter);
        }
        else
        {
            if (propertyType.IsNullable())
                propertyAccess = Expression.Property(propertyAccess, "Value");

            propertyAccess = Expression.Property(propertyAccess, "Date");

            var value = Expression.Constant(dateFilter.Value.Value.Date, typeof(DateTime));
            var equal = Expression.Equal(propertyAccess, value);

            // x => x.Property.Date == FilterValue.Value.Date
            return Expression.Lambda<Func<T, bool>>(equal, parameter);
        }
    }

    public Expression<Func<T, bool>> DateRangeFilterExpression<T>(SimpleDateRangeFilter<T> dateRangeFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = ExpressionHelper.PropertyAccess(dateRangeFilter, parameter);
        var propertyType = propertyAccess.Type;
        
        if (propertyType == typeof(DateTime?) || propertyType == typeof(DateOnly?))
            propertyAccess = Expression.Property(propertyAccess, "Value");

        if (!dateRangeFilter.IncludeTime && propertyType != typeof(DateOnly) && propertyType != typeof(DateOnly?)) 
            propertyAccess = Expression.Property(propertyAccess, "Date");

        var low = (propertyType == typeof(DateOnly) || propertyType == typeof(DateOnly?))
            ? Expression.Constant(DateOnly.FromDateTime(dateRangeFilter.LowValue.Value), typeof(DateOnly))
            : Expression.Constant(dateRangeFilter.LowValue.Value, typeof(DateTime));
        
        var high = (propertyType == typeof(DateOnly) || propertyType == typeof(DateOnly?))
            ? Expression.Constant(DateOnly.FromDateTime(dateRangeFilter.HighValue.Value), typeof(DateOnly))
            : Expression.Constant(dateRangeFilter.HighValue.Value, typeof(DateTime));

        var greaterThanLow = dateRangeFilter.Inclusive
            ? Expression.GreaterThanOrEqual(propertyAccess, low)
            : Expression.GreaterThan(propertyAccess, low);

        var lessThanHigh = dateRangeFilter.Inclusive
            ? Expression.LessThanOrEqual(propertyAccess, high)
            : Expression.LessThan(propertyAccess, high);

        var and = Expression.And(greaterThanLow, lessThanHigh);

        // x => x.Property >= Low && x.Property <= High
        return Expression.Lambda<Func<T, bool>>(and, parameter);
    }

    public Expression<Func<T, bool>> EnumFilterExpression<T>(EnumFilter<T> enumFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = ExpressionHelper.PropertyAccess(enumFilter, parameter);

        var enumValues = EnumHelper.ParseEnumList(enumFilter.EnumType, enumFilter.SelectedValues);
        var enumListType = typeof(List<>).MakeGenericType(enumFilter.EnumType);
        var contains = Expression.Call(Expression.Constant(enumValues), enumListType.GetMethod("Contains")!, propertyAccess);
        
        // x => listOfEnumValues.Contains(x.EnumProperty)
        return Expression.Lambda<Func<T, bool>>(contains, parameter);
    }
}