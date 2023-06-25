namespace SimpleBlazorGrid.Interfaces
{
    public interface IDataGridSource<T>
    {
        public T[] Items();
    }
}