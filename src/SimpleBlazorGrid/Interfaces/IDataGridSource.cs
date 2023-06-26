using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.Interfaces
{
    public interface IDataGridSource<T>
    {
        public FilterOptions FilterOptions { get; set; }
        public SearchOptions SearchOptions { get; set; }
        public PageOptions PageOptions { get; set; }

        public T[] Items();
    }
}