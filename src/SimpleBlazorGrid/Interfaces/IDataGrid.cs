namespace SimpleBlazorGrid.Interfaces
{
    public interface IDataGrid<T>
    {
        void AddColumn(IColumn<T> column);
        void AddFilter(IFilter<T> filter);
    }
}