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
            string step) : base(id, property)
        {
            Min = min;
            Max = max;
            Step = step;
        }
        
        public string Min { get; }
        public string Max { get; }
        public string Step { get; }
        public string MinValue { get; private set; }
        public string MaxValue { get; private set; }
        
        public override IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items)
        {
            throw new System.NotImplementedException();
        }

        public void SetValues(string min, string max)
        {
            MinValue = min;
            MaxValue = max;
        }
    }
}