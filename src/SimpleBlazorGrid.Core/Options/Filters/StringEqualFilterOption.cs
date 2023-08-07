using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleBlazorGrid.Options.Filters
{
    public class StringEqualFilterOption : FilterOption
    {
        public StringEqualFilterOption(Guid id, string property
            ) : base(id, property)
        {
        }

        public string Value { get; private set; }

        public override Expression<Func<T, bool>> Filter<T>()
        {
            var parameterExpression = Expression.Parameter(typeof(T), "x");
            
            Expression propertyAccess = parameterExpression;
            foreach (var property in Property.Split('.'))
            {
                propertyAccess = Expression.Property(propertyAccess, property);
            }
            
            var value = Expression.Constant(Value, typeof(string));
            
            var equality = Expression.Equal(propertyAccess, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameterExpression);

            return lambda;
        }

        public void SetValue(string value) => Value = value;
    }
}