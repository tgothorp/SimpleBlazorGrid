using System.Diagnostics.CodeAnalysis;

namespace SimpleBlazorGrid.Core.Tests.Unit.Filters;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
public class NumericFilterTests
{
    public static TheoryData<decimal, string, bool> DecimalData => new()
    {
        {12.0m, "12", true },
        {12.0m, "12.00", true },
        {12.101m, "12.101", true },
        {12.101m, "12.10100000", true },
        {1.11m, "1.12", false },
        {1m, "2", false },
        {1m, "1.00000000000001", false },
    };
    
    [Theory]
    [MemberData(nameof(DecimalData))]
    public async Task NumericFilter_DecimalFilter(decimal propertyValue, string testValue, bool shouldMatch)
    {
        var numericFilter = new SimpleNumericFilter<TestSetup.DecimalRecord>()
        {
            For = x => x.Decimal,
            Value = testValue
        };

        var testRecord = new TestSetup.DecimalRecord(propertyValue);
        var dataSource = TestSetup.CreateDataSource(testRecord, numericFilter);

        var result = await dataSource.Items();
        result.ShouldNotBeNull();
        result.Length.ShouldBe(shouldMatch ? 1 : 0);
    }
    
    public static TheoryData<float, string, bool> FloatData => new()
    {
        {12.0f, "12", true },
        {12.0f, "12.00", true },
        {12.101f, "12.101", true },
        {12.101f, "12.10100000", true },
        {1.11f, "1.12", false },
        {1f, "2", false },
    };
    
    [Theory]
    [MemberData(nameof(FloatData))]
    public async Task NumericFilter_FloatFilter(float propertyValue, string testValue, bool shouldMatch)
    {
        var numericFilter = new SimpleNumericFilter<TestSetup.FloatRecord>()
        {
            For = x => x.Float,
            Value = testValue
        };

        var testRecord = new TestSetup.FloatRecord(propertyValue);
        var dataSource = TestSetup.CreateDataSource(testRecord, numericFilter);

        var result = await dataSource.Items();
        result.ShouldNotBeNull();
        result.Length.ShouldBe(shouldMatch ? 1 : 0);
    }
    
    public static TheoryData<double, string, bool> DoubleData => new()
    {
        {12.0, "12", true },
        {12.0, "12.00", true },
        {12.101, "12.101", true },
        {12.101, "12.10100000", true },
        {1.00000000000001, "1.00000000000001", true },
        {1.11, "1.12", false },
        {1, "2", false },
        {1.00000000000001, "1.00000000000002", false },
    };
    
    [Theory]
    [MemberData(nameof(DoubleData))]
    public async Task NumericFilter_DoubleFilter(double propertyValue, string testValue, bool shouldMatch)
    {
        var numericFilter = new SimpleNumericFilter<TestSetup.DoubleRecord>()
        {
            For = x => x.Double,
            Value = testValue
        };

        var testRecord = new TestSetup.DoubleRecord(propertyValue);
        var dataSource = TestSetup.CreateDataSource(testRecord, numericFilter);

        var result = await dataSource.Items();
        result.ShouldNotBeNull();
        result.Length.ShouldBe(shouldMatch ? 1 : 0);
    }
}