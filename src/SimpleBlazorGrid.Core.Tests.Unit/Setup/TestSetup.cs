using SimpleBlazorGrid.DataSource;
using SimpleBlazorGrid.Filters;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.Core.Tests.Unit.Setup;

public static class TestSetup
{
    public record StringRecord(string String);
    public record DateTimeRecord(DateTime DateTime);
    public record DateOnlyRecord(DateOnly DateOnly);
    public record DecimalRecord(decimal Decimal);
    public record FloatRecord(float Float);
    public record DoubleRecord(double Double);
    
    public static SimpleGridEnumerableDataSource<T> CreateDataSource<T>(T record, params Filter<T>[] filters)
        => CreateDataSource(new List<T> { record }, filters);
    
    public static SimpleGridEnumerableDataSource<T> CreateDataSource<T>(IEnumerable<T> records, params Filter<T>[] filters)
    {
        return new SimpleGridEnumerableDataSource<T>(records)
        {
            Filters = filters,
            PageOptions = new PageOptions { ItemsPerPage = 50 },
            SortOptions = new SortOptions()
        };
    }
}