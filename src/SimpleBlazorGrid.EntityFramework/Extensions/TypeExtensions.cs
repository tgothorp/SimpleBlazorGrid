using System;
using System.Linq;

namespace SimpleBlazorGrid.EntityFramework.Extensions;

public static class TypeExtensions
{
    public static bool IsNumericType(this Type type)
    {
        Type[] numericTypes = { typeof(int), typeof(float), typeof(double), typeof(decimal) };
        return numericTypes.Contains(type) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && Nullable.GetUnderlyingType(type).IsNumericType());
    }

    public static bool IsNullable(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}