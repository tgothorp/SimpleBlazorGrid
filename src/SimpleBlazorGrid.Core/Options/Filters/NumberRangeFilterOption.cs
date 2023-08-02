using System;
using System.Collections.Generic;

namespace SimpleBlazorGrid.Options.Filters
{
    public class NumberRangeFilterOption : FilterOption
    {
        public NumberRangeFilterOption(Guid id, 
            string property,
            string min,
            string max,
            string step)
        {
            Id = id;
            Property = property;

            Min = min;
            Max = max;
            Step = step;
        }
        
        public string Min { get; }
        public string Max { get; }
        public string Step { get; }
        public string Value { get; private set; }
        
        public override IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items)
        {
            throw new System.NotImplementedException();
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