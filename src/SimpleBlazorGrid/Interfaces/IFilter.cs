using System;
using SimpleBlazorGrid.Options.Filters;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IFilter<TType>
    {
        public Guid Id { get; }
        public string Property { get; set; }
        public FilterOption FilterOption { get; }
        public bool Active { get; set; }

        void SetActive(bool isActive);
    }
}