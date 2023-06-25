namespace AnotherBlazorGrid.Interfaces
{
    public interface IDataGridSource<T>
    {
        public T[] Items();
    }
}