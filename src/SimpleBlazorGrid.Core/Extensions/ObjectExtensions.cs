using System;
using System.Reflection;

namespace SimpleBlazorGrid.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetPropertyValue(this object @object, Type objectType, string propertyName)
        {
            var propertyInfo = objectType.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                var propertyValue = propertyInfo.GetValue(@object);
                return propertyValue;
            }

            return null;
        }
    }
}