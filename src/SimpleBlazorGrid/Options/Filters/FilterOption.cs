using System;
using System.Collections.Generic;

namespace SimpleBlazorGrid.Options.Filters
{
    public abstract class FilterOption
    {
        protected FilterOption()
        {
            Id = Guid.NewGuid();
        }
        
        public readonly Guid Id;

        public string Property { get; set; }
        public abstract IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items);
    }
}