using Microsoft.AspNetCore.Components;
using SimpleBlazorGrid.Enums;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IColumn<TType>
    {
        public string Property { get; set; }
        public string Heading { get; set; }
        public Format Format { get; set; }
        public RenderFragment<TType> SimpleColumnTemplate { get; set; }
    }
}