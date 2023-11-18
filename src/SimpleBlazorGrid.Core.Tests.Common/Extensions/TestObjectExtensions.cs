using System.Linq.Expressions;

namespace SimpleBlazorGrid.Core.Tests.Common.Extensions;

public static class TestObjectExtensions
{
    public static T SetProperty<T, TP>(this T instance, Expression<Func<T, TP>> accessor, TP value)
    {
        accessor.GetPropertyInfo().SetValue(instance, value);
        return instance;
    }
}