using System;
using System.Linq.Expressions;
using System.Reflection;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.Helpers
{
    public static class ExpressionHelper
    {
        public static Expression PropertyAccess<T>(string propertyName, ParameterExpression parameter)
        {
            Expression propertyAccess = parameter;
            foreach (var property in propertyName.Split('.'))
            {
                propertyAccess = Expression.Property(propertyAccess, property);
            }

            return propertyAccess;
        }
        
        public static Expression PropertyAccess<T>(Filter<T> numericRangeFilter, ParameterExpression parameter)
        {
            Expression propertyAccess = parameter;
            foreach (var property in numericRangeFilter.PropertyName.Split('.'))
            {
                propertyAccess = Expression.Property(propertyAccess, property);
            }

            return propertyAccess;
        }

        public static PropertyInfo GetPropertyInfo<T>(Expression<Func<T, object>> expression)
        {
            var member = ExtractMemberExpression(expression);
            var property = member.Member as PropertyInfo;

            return property;
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
            return expression switch
            {
                UnaryExpression unaryExpression => ExtractMemberExpression(unaryExpression.Operand),
                LambdaExpression lambdaExpression => ExtractMemberExpression(lambdaExpression.Body),
                MemberExpression memberExpression => memberExpression,
                _ => null
            };
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