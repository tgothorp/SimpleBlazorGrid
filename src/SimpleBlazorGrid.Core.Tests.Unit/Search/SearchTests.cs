using SimpleBlazorGrid.DataSource;
using SimpleBlazorGrid.Options;

namespace SimpleBlazorGrid.Core.Tests.Unit.Search;

public class SearchTests
{
    private record SearchTestClass(string Property1, string Property2, string Property3, string Property4);

    [Theory]
    [InlineData("ABC1", 1)]
    [InlineData("BC", 2)]
    [InlineData("bc", 2)]
    [InlineData("1", 4)]
    [InlineData("g", 0)]
    [InlineData("AD", 0)]
    public async Task SingleProperty_SearchesCorrectly(string query, int expectedResults)
    {
        var data = new List<SearchTestClass>
        {
            new("ABC1", "_", "_", "_"),
            new("BCD1", "_", "_", "_"),
            new("CDE1", "_", "_", "_"),
            new("DEF1", "_", "_", "_"),
        };

        var source = SetupDataSource(data, query, "Property1");

        var result = await source.Items();
        result.Length.ShouldBe(expectedResults);
    }

    [Theory]
    [InlineData("XYZ2", 1)]
    [InlineData("ABC1", 2)]
    [InlineData("c", 3)]
    [InlineData("1", 4)]
    [InlineData("XXX", 0)]
    public async Task TwoProperties_SearchesCorrectly(string query, int expectedResults)
    {
        var data = new List<SearchTestClass>
        {
            new("ABC1", "ABC1", "_", "_"),
            new("BCD1", "ABC1", "_", "_"),
            new("CDE1", "XYZ2", "_", "_"),
            new("DEF1", "EFG1", "_", "_"),
        };
        
        var source = SetupDataSource(data, query, "Property1", "Property2");

        var result = await source.Items();
        result.Length.ShouldBe(expectedResults);
    }
    
    [Theory]
    [InlineData("ABC1", 2)]
    [InlineData("4", 2)]
    [InlineData("D", 2)]
    [InlineData("1", 4)]
    public async Task MultipleProperties_SearchesCorrectly(string query, int expectedResults)
    {
        var data = new List<SearchTestClass>
        {
            new("ABC1", "ABC1", "ABC1", "ABC1"),
            new("ABC1", "BCD1", "CDE1", "DEF1"),
            new("A1", "B2", "C3", "D4"),
            new("1", "2", "3", "4"),
            new("X", "T", "Z", "_"),
        };
        
        var source = SetupDataSource(data, query, "Property1", "Property2", "Property3", "Property4");

        var result = await source.Items();
        result.Length.ShouldBe(expectedResults);
    }

    private SimpleGridEnumerableDataSource<SearchTestClass> SetupDataSource(List<SearchTestClass> data, string query, params string[] properties)
    {
        var source = new SimpleGridEnumerableDataSource<SearchTestClass>(data)
        {
            SearchOptions = new SearchOptions { Columns = properties.ToHashSet(), Query = query},
            PageOptions = new PageOptions { ItemsPerPage = 50 }
        };
        return source;
    }
}