using SimpleBlazorGrid.Enums;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IFilter<TType>
    {
        public string Property { get; set; }
        public FilterType FilterType { get; }
    }
}