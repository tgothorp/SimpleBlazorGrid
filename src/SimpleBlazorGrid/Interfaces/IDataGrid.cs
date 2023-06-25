namespace SimpleBlazorGrid.Interfaces
{
    public interface IDataGrid<T>
    {
        void AddColumn(IColumn<T> column);
    }
}