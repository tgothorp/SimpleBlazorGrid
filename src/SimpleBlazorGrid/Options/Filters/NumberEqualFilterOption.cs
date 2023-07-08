using System;
using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }
    }
}