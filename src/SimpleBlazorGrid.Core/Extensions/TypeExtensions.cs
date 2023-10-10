using System;
using System.Linq;

namespace SimpleBlazorGrid.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if a given type is a int, float, double or decimal or a nullable version of these types
        /// </summary>
        public static bool IsNumericType(this Type type)
        {
            Type[] numericTypes = { typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(short), typeof(ushort), typeof(float), typeof(double), typeof(decimal) };
            return numericTypes.Contains(type) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && Nullable.GetUnderlyingType(type).IsNumericType());
        }

        /// <summary>
        /// Determines if a given types is a DateTime or DateOnly or a nullable version of these types
        /// </summary>
        public static bool IsDateType(this Type type)
        {
            Type[] dateTypes = { typeof(DateOnly), typeof(DateTime) };
            return dateTypes.Contains(type) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && Nullable.GetUnderlyingType(type).IsDateType());
        }

        /// <summary>
        /// Determines if the given type is a nullable type, eg. decimal?
        /// </summary>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}