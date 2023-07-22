using System.Collections.Generic;

namespace SimpleBlazorGrid.Options
{
    public class SearchOptions
    {
        public string Query { get; set; }
        public List<string> PropertiesToQuery { get; set; }
    }
}