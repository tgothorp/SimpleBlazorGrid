using Microsoft.AspNetCore.Components;

namespace AnotherBlazorGrid.Interfaces
{
    public interface IColumn<TType>
    {
        public string Property { get; set; }
        public string Heading { get; set; }
        public RenderFragment<TType> ABColumnFormat { get; set; }
    }
}