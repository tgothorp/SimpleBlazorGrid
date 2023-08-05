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
            string step) : base(id, property)
        {
            Min = min ?? int.MinValue.ToString();
            Max = max ?? int.MaxValue.ToString();
            Step = step ?? "0.01";
        }

        public string Min { get; set; }
        public string Max { get; set; }
        public string Step { get; set; }
        public string Value { get; private set; }

        public override Expression<Func<T, bool>> Filter<T>()
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, Property);

            // Get the property type dynamically
            var propertyInfo = typeof(T).GetProperty(Property);
            var propertyType = propertyInfo.PropertyType;

            var value = Expression.Constant(Convert.ChangeType(Value, propertyType), propertyType);
            var equality = Expression.Equal(property, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return lambda;
        }

        public void SetValue(string value)
        {
            Value = value;
        }
    }
}