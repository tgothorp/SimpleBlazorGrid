using System;
using System.Collections.Generic;

namespace SimpleBlazorGrid.Options.Filters
{
    public abstract class FilterOption
    {
        protected FilterOption(Guid id, string property)
        {
            Id = id;
            Property = property;
        }
        
        public Guid Id { get; protected init; }
        public string Property { get; protected init; }

        public abstract IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items);
    }
}