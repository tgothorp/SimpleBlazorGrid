using System;
using System.Linq.Expressions;
using SimpleBlazorGrid.Options.Filters;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IFilter<TType>
    {
        public Guid Id { get; }

        public string PropertyName { get; }
        public Expression<Func<TType, object>> For { get; set; }

        public FilterOption FilterOption { get; }
        public bool Active { get; }

        void SetActive(bool isActive);
    }
}