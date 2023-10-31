using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleBlazorGrid.Extensions;

public static class ExpressionExtensions
{
    public static PropertyInfo GetPropertyInfo<T, P>(this Expression<Func<T, P>> accessor)
    {
        var member = accessor.Body as MemberExpression;
        var property = member.Member as PropertyInfo;

        return property;
    }
}