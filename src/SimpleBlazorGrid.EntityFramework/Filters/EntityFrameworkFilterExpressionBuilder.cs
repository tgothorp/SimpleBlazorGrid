using System;
using System.Linq.Expressions;
using SimpleBlazorGrid.EntityFramework.Extensions;
using SimpleBlazorGrid.EntityFramework.Helpers;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.EntityFramework.Filters;

public class EntityFrameworkFilterExpressionBuilder
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
            SimpleEnumFilter<T, Enum> simpleEnumFilter => EnumFilterExpression(simpleEnumFilter),
            _ => throw new ArgumentException($"There is no appropriate method to handle generation of a filter expression for the provided filter", nameof(filter))
        };
    }

    private Expression<Func<T, bool>> StringFilterExpression<T>(SimpleStringFilter<T> stringFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in stringFilter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
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

    private Expression<Func<T, bool>> NumericFilterExpression<T>(SimpleNumericFilter<T> numericFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in numericFilter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }

        var value = Expression.Constant(NumericHelper.ConvertStringToNumericType(numericFilter.Value, propertyAccess.Type), propertyAccess.Type);
        var equality = Expression.Equal(propertyAccess, value);
        return Expression.Lambda<Func<T, bool>>(equality, parameter);
    }

    private Expression<Func<T, bool>> NumericRangeFilterExpression<T>(SimpleNumericRangeFilter<T> numericRangeFilter)
    {
        throw new NotImplementedException();
    }

    private Expression<Func<T, bool>> DateFilterExpression<T>(SimpleDateFilter<T> dateFilter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression propertyAccess = parameter;
        foreach (var property in dateFilter.PropertyName.Split('.'))
        {
            propertyAccess = Expression.Property(propertyAccess, property);
        }

        var propertyType = propertyAccess.Type;

        if (dateFilter.IncludeTime || (Nullable.GetUnderlyingType(propertyType) ?? propertyType) == typeof(DateOnly))
        {
            var value = Expression.Constant(DateTimeHelper.ConvertDateTimeToTargetType(propertyType, dateFilter.Value), propertyType);
            var equal = Expression.Equal(propertyAccess, value);

            return Expression.Lambda<Func<T, bool>>(equal, parameter);
        }
        else
        {
            if (propertyType.IsNullable())
                propertyAccess = Expression.Property(propertyAccess, "Value");

            propertyAccess = Expression.Property(propertyAccess, "Date");

            var value = Expression.Constant(dateFilter.Value.Value.Date, typeof(DateTime));
            var equal = Expression.Equal(propertyAccess, value);

            return Expression.Lambda<Func<T, bool>>(equal, parameter);
        }
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