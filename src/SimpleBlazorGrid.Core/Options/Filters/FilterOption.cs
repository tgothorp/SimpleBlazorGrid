using System;
using System.Collections.Generic;

namespace SimpleBlazorGrid.Options.Filters
{
    public abstract class FilterOption
    {
        public Guid Id { get; protected init; }
        public string Property { get; protected init; }

        public abstract IEnumerable<T> ApplyFilter<T>(IEnumerable<T> items);
        public abstract void SetValue(object value);
    }
}