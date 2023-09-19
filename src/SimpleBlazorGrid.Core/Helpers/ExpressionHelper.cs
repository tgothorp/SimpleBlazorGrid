using System;
using System.Linq.Expressions;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.Helpers
{
    public static class ExpressionHelper
    {
        public static Expression PropertyAccess<T>(Filter<T> numericRangeFilter, ParameterExpression parameter)
        {
            Expression propertyAccess = parameter;
            foreach (var property in numericRangeFilter.PropertyName.Split('.'))
            {
                propertyAccess = Expression.Property(propertyAccess, property);
            }

            return propertyAccess;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            if (expression is null)
                return null;

            var memberExpression = ExtractMemberExpression(expression.Body);
            return GetPropertyPathFromExpression(memberExpression);
        }

        private static MemberExpression ExtractMemberExpression(Expression expression)
        {
            if (expression is UnaryExpression unaryExpression)
            {
                return ExtractMemberExpression(unaryExpression.Operand);
            }
            else if (expression is MemberExpression memberExpression)
            {
                return memberExpression;
            }

            return null;
        }

        private static string GetPropertyPathFromExpression(MemberExpression memberExpression)
        {
            var path = memberExpression.Member.Name;
            while (memberExpression.Expression is MemberExpression nextMemberExpression)
            {
                path = nextMemberExpression.Member.Name + "." + path;
                memberExpression = nextMemberExpression;
            }

            return path;
        }
    }
}