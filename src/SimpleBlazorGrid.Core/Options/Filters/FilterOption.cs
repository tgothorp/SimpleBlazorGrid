using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public abstract Expression<Func<T, bool>> Filter<T>();
    }
}