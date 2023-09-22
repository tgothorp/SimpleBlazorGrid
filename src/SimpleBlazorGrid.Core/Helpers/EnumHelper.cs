using System;
using System.Collections.Generic;

namespace SimpleBlazorGrid.Helpers;

public static class EnumHelper
{
    public static object ParseEnumList(Type enumType, List<string> stringValues)
    {
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("The provided type is not an enum type.");
        }

        var listType = typeof(List<>).MakeGenericType(enumType);
        var listInstance = Activator.CreateInstance(listType);
        var addMethod = listType.GetMethod("Add");

        foreach (var stringValue in stringValues)
        {
            Enum.TryParse(enumType, stringValue, true, out var parsed);
            addMethod!.Invoke(listInstance, new[] { parsed });
        }

        return listInstance;
    }
}