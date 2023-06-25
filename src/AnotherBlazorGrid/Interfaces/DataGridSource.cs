using System.Collections.Generic;
using System.Linq;

namespace AnotherBlazorGrid.Interfaces
{
    public class DataGridEnumerableSource<T> : IDataGridSource<T>
    {
        private IEnumerable<T> Source { get; }

        public DataGridEnumerableSource(IEnumerable<T> source)
        {
            Source = source;
        }

        public T[] Items()
        {
            // TODO, filtering, sorting etc.
            return Source.ToArray();
        }
    }
}