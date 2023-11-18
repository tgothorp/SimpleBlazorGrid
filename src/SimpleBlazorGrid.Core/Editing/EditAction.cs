using System;
using System.Linq;
using System.Linq.Expressions;
using SimpleBlazorGrid.Exceptions;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.Editing;

public class EditAction<T>
{
    public Expression<Func<T, object>> For { get; }
    public Type PropertyType { get; }
    public bool PropertyTypeIsNullable { get; }
    public object NewValue { get; }

    private TypeCode[] _unsupportedTypes = { TypeCode.Byte, TypeCode.Char, TypeCode.Empty, TypeCode.SByte, TypeCode.DBNull };

    public EditAction(Expression<Func<T, object>> @for, object newValue)
    {
        For = @for;
        NewValue = newValue;

        var type = ExpressionHelper.GetPropertyInfo(For).PropertyType;
        PropertyType = Nullable.GetUnderlyingType(type) ?? type;
        PropertyTypeIsNullable = type.IsNullable();

        if (!PropertyType.IsValueType && PropertyType != typeof(string))
            throw new SimpleGridException($"Cannot edit property {For}. Editing is only supported for value types.");
        
        if (_unsupportedTypes.Contains(Type.GetTypeCode(PropertyType)))
            throw new SimpleGridException($"Cannot edit property {For}. Editing is not supported for the following types {string.Join(',', _unsupportedTypes)}");
    }

    public void Apply(ref T instance)
    {
        object convertedNewValue;
        var propertyInfo = ExpressionHelper.GetPropertyInfo(For);

        if (NewValue is null && PropertyTypeIsNullable)
        {
            propertyInfo.SetValue(instance, null);
            return;
        }
        
        try {
            convertedNewValue = Convert.ChangeType(NewValue, PropertyType);
        }
        catch (FormatException) {
            convertedNewValue = PropertyTypeIsNullable 
                ? null
                : Activator.CreateInstance(PropertyType);
        }

        propertyInfo.SetValue(instance, convertedNewValue);
    }
}