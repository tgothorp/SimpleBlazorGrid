using System;
using System.Reflection;

namespace SimpleBlazorGrid.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Get the property info for a given property name even if deeply nested, for example; MyObject.Property1.Property2.Property3
        /// </summary>
        public static PropertyInfo GetPropertyInfoRecursively(this Type @type, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}