using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SimpleBlazorGrid.Extensions;

namespace SimpleBlazorGrid.Options.Filters
{
    public class DateTimeEqualFilterOption : FilterOption
    {
        public DateTimeEqualFilterOption(Guid id, string property) 
            : base(id, property)
        {
        }

        public DateTime? Value { get; private set; }

        public override Expression<Func<T, bool>> Filter<T>()
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, Property);

            var value = Expression.Constant(Value.Value, typeof(DateTime));
            var equal = Expression.Equal(property, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            return lambda;
        }

        public void SetValue(DateTime? dateTime)
        {
            Value = dateTime;
        }
    }
}