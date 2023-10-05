using System.Collections.Generic;

namespace SimpleBlazorGrid.Options;

public class SearchOptions
{
    public string Query { get; set; }
    public HashSet<string> Columns { get; set; } = new();
}