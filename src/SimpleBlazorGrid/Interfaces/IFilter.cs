using SimpleBlazorGrid.Enums;
using SimpleBlazorGrid.Options.Filters;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IFilter<TType>
    {
        public string Property { get; set; }
        public FilterType FilterType { get; }
        public FilterOption FilterOption { get; }
    }
}