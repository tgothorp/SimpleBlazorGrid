using System;
using System.Linq.Expressions;

namespace SimpleBlazorGrid.Helpers
{
    public static class ExpressionHelper
    {
        public static object GetNestedPropertyValue<TObject>(TObject obj, string propertyName)
        {
            var parameterExpression = Expression.Parameter(typeof(TObject), "obj");
            Expression propertyAccess = parameterExpression;

            foreach (var property in propertyName.Split('.'))
            {
                propertyAccess = Expression.Property(propertyAccess, property);
            }

            var lambdaExpression = Expression.Lambda(propertyAccess, parameterExpression);
            var getter = lambdaExpression.Compile();

            return getter.DynamicInvoke(obj);
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