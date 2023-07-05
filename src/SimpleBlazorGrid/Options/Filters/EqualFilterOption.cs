using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleBlazorGrid.Options.Filters
{
    public class EqualFilterOption : FilterOption
    {
        public EqualFilterOption(string property, bool ignoreCase = true) : base()
        {
            Property = property;
            IgnoreCase = ignoreCase;
        }
        public string Value { get; set; }
        public bool IgnoreCase { get; }

        public override IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, Property);
            var value = Expression.Constant(Value, typeof(string));

            Expression<Func<T, bool>> lambda;

            if (IgnoreCase)
            {
                // string.Equals(string1, string2, StringComparison)
                var equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(string), typeof(StringComparison) });
                var equality = Expression.Call(null, equalsMethod, property, value, Expression.Constant(StringComparison.OrdinalIgnoreCase));
                lambda = Expression.Lambda<Func<T, bool>>(equality, param);
            }
            else
            {
                var toLower = Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
                var equality = Expression.Equal(toLower, value);
                lambda = Expression.Lambda<Func<T, bool>>(equality, param);
            }

            return items.Where(lambda.Compile());
        }
    }
}