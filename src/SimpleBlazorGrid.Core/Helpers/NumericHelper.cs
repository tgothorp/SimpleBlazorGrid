﻿using System;
using SimpleBlazorGrid.Extensions;

namespace SimpleBlazorGrid.Helpers;

public static class NumericHelper
{
    public static object ConvertStringToNumericType(string inputString, Type targetType)
    {
        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (underlyingType.IsNumericType())
        {
            var parseMethod = underlyingType.GetMethod("Parse", new[] { typeof(string) });

            if (parseMethod != null)
            {
                var parsedValue = parseMethod.Invoke(null, new object[] { inputString });
                return targetType.IsNullable() ? Activator.CreateInstance(targetType, parsedValue) : parsedValue;
            }
        }

        throw new Exception();
    }
}