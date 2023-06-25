using System;
using System.Linq.Expressions;

namespace AnotherBlazorGrid.Helpers
{
    public static class ExpressionHelper
    {
        public static Delegate GetPropertyByName(Type objectType, object @object, string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(objectType, "obj");
            Expression propertyExpression = Expression.Property(parameter, propertyName);
        
            // Potential null ref if we do not provide a valid property name 
            var propertyType = objectType.GetProperty(propertyName)?.PropertyType;
            
            LambdaExpression lambda = Expression.Lambda(
                typeof(Func<,>).MakeGenericType(objectType, propertyType),
                propertyExpression,
                parameter
            );

            Delegate getter = lambda.Compile();

            return getter;
        }
    }
}