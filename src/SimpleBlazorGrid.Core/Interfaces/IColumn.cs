using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using SimpleBlazorGrid.Enums;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IColumn<TType>
    {
        public string PropertyName { get; set; }
        public Expression<Func<TType, object>> For { get; set; }
        
        public string Heading { get; set; }
        public Format Format { get; set; }
        public bool Sortable { get; set; }
        public int? Width { get; set; }
        public RenderFragment<TType> SimpleColumnTemplate { get; set; }
    }
}