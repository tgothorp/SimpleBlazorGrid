using System;
using System.Linq.Expressions;

namespace SimpleBlazorGrid.Filters;

public abstract class FilterExpressionBuilder
{
    public abstract Expression<Func<T, bool>> GetFilterExpression<T>(Filter<T> filter);
}