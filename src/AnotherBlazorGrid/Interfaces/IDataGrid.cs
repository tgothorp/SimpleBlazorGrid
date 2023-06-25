namespace AnotherBlazorGrid.Interfaces
{
    public interface IDataGrid<T>
    {
        void AddColumn(IColumn<T> column);
    }
}