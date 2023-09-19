using System;

namespace SimpleBlazorGrid.Helpers;

public static class DateTimeHelper
{
    public static object ConvertDateTimeToTargetType(Type targetType, DateTime? value)
    {
        if (targetType == typeof(DateTime?))
            return value;
        
        if (targetType == typeof(DateTime))
            return value.Value;

        if (targetType == typeof(DateOnly))
            return DateOnly.FromDateTime(value.Value);

        if (targetType == typeof(DateOnly?))
            return DateOnly.FromDateTime(value.Value);

        throw new Exception();
    }
}