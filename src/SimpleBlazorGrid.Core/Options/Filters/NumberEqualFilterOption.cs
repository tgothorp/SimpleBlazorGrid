using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleBlazorGrid.Options.Filters
{
    public class NumberEqualFilterOption : FilterOption
    {
        public NumberEqualFilterOption(Guid id, 
            string property,
            string min,
            string max,
            string step)
        {
            Id = id;
            Property = property;

            Min = min ?? int.MinValue.ToString();
            Max = max ?? int.MaxValue.ToString();
            Step = step ?? "0.01";
        }

        public string Min { get; set; }
        public string Max { get; set; }
        public string Step { get; set; }
        public string Value { get; set; }

        public override IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, Property);

            // Get the property type dynamically
            var propertyInfo = typeof(T).GetProperty(Property);
            var propertyType = propertyInfo.PropertyType;

            var value = Expression.Constant(Convert.ChangeType(Value, propertyType), propertyType);
            var equality = Expression.Equal(property, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);
            
            return items.Where(lambda.Compile());
        }

        public override void SetValue(object value)
        {
            Value = value switch
            {
                string @string => @string,
                null => null,
                _ => throw new ArgumentException($"")
            };
        }
    }
}