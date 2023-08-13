using System;
using System.Linq.Expressions;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.EntityFramework.Filters;

public class EntityFrameworkFilterExpressionBuilder : FilterExpressionBuilder
{
    public override Expression<Func<T, bool>> GetFilterExpression<T>(Filter<T> filter)
    {
        throw new NotImplementedException();
    }
}