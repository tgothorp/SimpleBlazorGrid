using System.Collections.Generic;

namespace SimpleBlazorGrid.Filters;

public abstract class EnumFilter<T> : Filter<T>
{
    public virtual List<string> SelectedValues { get; set; }
    public virtual bool AllowMultiple { get; set; }
}