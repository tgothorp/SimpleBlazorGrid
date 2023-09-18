using System.Diagnostics.CodeAnalysis;

namespace SimpleBlazorGrid.Core.Tests.Unit.Filters;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]    
public class StringFilterTests
{
    public record TestRecord(string StringProperty);

    [Theory]
    [InlineData("UPPERCASE", "UPPERCASE", true)]
    [InlineData("lowercase", "lowercase", true)]
    [InlineData("snake_case", "snake_case", true)]
    [InlineData("UPPERCASE", "lowercase", false)]
    [InlineData("Lowercase", "lowercase", false)]
    public async Task StringFilter_CaseSensitive(string propertyValue, string testValue, bool shouldMatch)
    {
        var stringFilter = new SimpleStringFilter<TestRecord>
        {
            For = x => x.StringProperty,
            IgnoreCase = false,
            Value = testValue
        };
        
        var testRecord = new TestRecord(propertyValue);
        var dataSource = TestSetup.CreateDataSource(testRecord, stringFilter);

        var result = await dataSource.Items(CancellationToken.None);
        result.ShouldNotBeNull();
        result.Length.ShouldBe(shouldMatch ? 1 : 0);
    }

    [Theory]
    [InlineData("UPPERCASE", "UPPERCASE", true)]
    [InlineData("UPPERCASE", "uppercase", true)]
    [InlineData("UPPERCASE", "upper_case", false)]
    public async Task StringFilter_CaseInsensitive(string propertyValue, string testValue, bool shouldMatch)
    {
        var stringFilter = new SimpleStringFilter<TestRecord>
        {
            For = x => x.StringProperty,
            IgnoreCase = true,
            Value = testValue,
        };
        
        var testRecord = new TestRecord(propertyValue);
        var dataSource = TestSetup.CreateDataSource(testRecord, stringFilter);

        var result = await dataSource.Items(CancellationToken.None);
        result.ShouldNotBeNull();
        result.Length.ShouldBe(shouldMatch ? 1 : 0);
    }
}