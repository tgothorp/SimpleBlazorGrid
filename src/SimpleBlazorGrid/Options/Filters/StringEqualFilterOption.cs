using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleBlazorGrid.Options.Filters
{
    public class StringEqualFilterOption : FilterOption
    {
        public StringEqualFilterOption(Guid id, string property) : base()
        {
            Id = id;
            Property = property;
        }
        public string Value { get; set; }

        public override IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, Property);
            var value = Expression.Constant(Value, typeof(string));

            // string.Equals(string1, string2, StringComparison)
            var equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(string), typeof(StringComparison) });
            var equality = Expression.Call(null, equalsMethod, property, value, Expression.Constant(StringComparison.OrdinalIgnoreCase));
            var lambda = Expression.Lambda<Func<T, bool>>(equality, param);
            
            return items.Where(lambda.Compile());
        }
    }
}