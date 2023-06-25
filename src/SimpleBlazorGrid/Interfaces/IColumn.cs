using Microsoft.AspNetCore.Components;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IColumn<TType>
    {
        public string Property { get; set; }
        public string Heading { get; set; }
        public RenderFragment<TType> SimpleColumnTemplate { get; set; }
    }
}