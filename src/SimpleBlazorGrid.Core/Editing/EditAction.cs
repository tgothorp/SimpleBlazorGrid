using System;
using System.Linq.Expressions;
using SimpleBlazorGrid.Extensions;

namespace SimpleBlazorGrid.Editing;

public class EditAction<T>
{
    public Expression<Func<T, object>> Property { get; set; }
    public object NewValue { get; set; }

    public void Apply(ref T instance)
    {
        Property.GetPropertyInfo().SetValue(instance, NewValue);
    }
}