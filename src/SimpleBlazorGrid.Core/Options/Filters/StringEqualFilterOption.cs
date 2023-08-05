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
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, Property);
            var value = Expression.Constant(Value, typeof(string));

            var equality = Expression.Equal(property, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, param);

            return lambda;
        }

        public void SetValue(string value) => Value = value;
    }
}