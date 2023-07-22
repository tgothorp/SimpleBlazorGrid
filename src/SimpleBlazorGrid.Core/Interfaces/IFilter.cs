using System;
using SimpleBlazorGrid.Options.Filters;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IFilter<TType>
    {
        public Guid Id { get; }
        public string Property { get; }
        public FilterOption FilterOption { get; }
        public bool Active { get; }

        void SetActive(bool isActive);
    }
}