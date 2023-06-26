namespace SimpleBlazorGrid.Options
{
    public class PageOptions
    {
        public int TotalItemCount { get; set; }
        
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
    }
}