using System;
using System.Linq.Expressions;
using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.Editing;

public class EditAction<T>
{
    public SimpleColumn<T> Column { get; set; }
    public object NewValue { get; set; }

    public void Apply(ref T instance)
    {
        var propertyInfo = ExpressionHelper.GetPropertyInfo(Column.For);
        propertyInfo.SetValue(instance, NewValue);
    }
}