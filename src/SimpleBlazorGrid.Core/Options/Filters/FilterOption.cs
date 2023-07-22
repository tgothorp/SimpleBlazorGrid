using System;
using System.Collections.Generic;

namespace SimpleBlazorGrid.Options.Filters
{
    public abstract class FilterOption
    {
        public Guid Id { get; set; }

        public string Property { get; set; }
        public abstract IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items);
    }
}