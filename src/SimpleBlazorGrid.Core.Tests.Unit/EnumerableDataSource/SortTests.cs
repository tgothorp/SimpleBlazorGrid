using SimpleBlazorGrid.Core.Tests.Common.Dto;
using SimpleBlazorGrid.Core.Tests.Common.Extensions;
using SimpleBlazorGrid.Core.Tests.Common.TableState;

namespace SimpleBlazorGrid.Core.Tests.Unit.EnumerableDataSource;

public class SortTests
{
    [Fact]
    public async Task SortProperty_SortByByte_ReturnsExpectedResult()
    {
        await SortByProperty(nameof(TestObject.ByteProperty), x => x.ByteProperty, true);
        await SortByProperty(nameof(TestObject.ByteProperty), x => x.ByteProperty, false);
        await SortByProperty(nameof(TestObject.NullableByteProperty), x => x.NullableByteProperty, true);
        await SortByProperty(nameof(TestObject.NullableByteProperty), x => x.NullableByteProperty, false);
    }
    
    [Fact]
    public async Task SortProperty_SortByChar_ReturnsExpectedResult()
    {
        await SortByProperty(nameof(TestObject.CharProperty), x => x.CharProperty, true);
        await SortByProperty(nameof(TestObject.CharProperty), x => x.CharProperty, false);
        await SortByProperty(nameof(TestObject.NullableCharProperty), x => x.NullableCharProperty, true);
        await SortByProperty(nameof(TestObject.NullableCharProperty), x => x.NullableCharProperty, false);
    }
    
    [Fact]
    public async Task SortProperty_SortByString_ReturnsExpectedResult()
    {
        await SortByProperty(nameof(TestObject.StringProperty), x => x.StringProperty, true);
        await SortByProperty(nameof(TestObject.StringProperty), x => x.StringProperty, false);
        await SortByProperty(nameof(TestObject.NullableStringProperty), x => x.NullableStringProperty, true);
        await SortByProperty(nameof(TestObject.NullableStringProperty), x => x.NullableStringProperty, false);
    }
    
    [Fact]
    public async Task SortProperty_SortByShort_ReturnsExpectedResult()
    {
        await SortByProperty(nameof(TestObject.ShortProperty), x => x.ShortProperty, true);
        await SortByProperty(nameof(TestObject.ShortProperty), x => x.ShortProperty, true);
        await SortByProperty(nameof(TestObject.UShortProperty), x => x.UShortProperty, true);
        await SortByProperty(nameof(TestObject.UShortProperty), x => x.UShortProperty, false);
        await SortByProperty(nameof(TestObject.NullableShortProperty), x => x.NullableShortProperty, true);
        await SortByProperty(nameof(TestObject.NullableShortProperty), x => x.NullableShortProperty, false);
        await SortByProperty(nameof(TestObject.NullableUShortProperty), x => x.NullableUShortProperty, false);
        await SortByProperty(nameof(TestObject.NullableUShortProperty), x => x.NullableUShortProperty, false);
    }

    [Fact]
    public async Task SortProperty_SortByInt_ReturnsExpectedResult()
    {
        await SortByProperty(nameof(TestObject.IntProperty), x => x.IntProperty, true);
        await SortByProperty(nameof(TestObject.IntProperty), x => x.IntProperty, true);
        await SortByProperty(nameof(TestObject.UIntProperty), x => x.UIntProperty, true);
        await SortByProperty(nameof(TestObject.UIntProperty), x => x.UIntProperty, false);
        await SortByProperty(nameof(TestObject.NullableIntProperty), x => x.NullableIntProperty, true);
        await SortByProperty(nameof(TestObject.NullableIntProperty), x => x.NullableIntProperty, false);
        await SortByProperty(nameof(TestObject.NullableUIntProperty), x => x.NullableUIntProperty, false);
        await SortByProperty(nameof(TestObject.NullableUIntProperty), x => x.NullableUIntProperty, false);
    }
    
    [Fact]
    public async Task SortProperty_SortByLong_ReturnsExpectedResult()
    {
        await SortByProperty(nameof(TestObject.LongProperty), x => x.LongProperty, true);
        await SortByProperty(nameof(TestObject.LongProperty), x => x.LongProperty, true);
        await SortByProperty(nameof(TestObject.ULongProperty), x => x.ULongProperty, true);
        await SortByProperty(nameof(TestObject.ULongProperty), x => x.ULongProperty, false);
        await SortByProperty(nameof(TestObject.NullableLongProperty), x => x.NullableLongProperty, true);
        await SortByProperty(nameof(TestObject.NullableLongProperty), x => x.NullableLongProperty, false);
        await SortByProperty(nameof(TestObject.NullableULongProperty), x => x.NullableULongProperty, false);
        await SortByProperty(nameof(TestObject.NullableULongProperty), x => x.NullableULongProperty, false);
    }

    private async Task SortByProperty(string propertyName, Func<TestObject, object> property, bool ascending)
    {
        // Arrange
        var data = TestObjectFactory.CreateManyWithAMixOfNulls(50);

        var minimum = data.OrderBy(property).Select(property).First();
        var maximum = data.OrderByDescending(property).Select(property).First();

        var setup = await new TableStateSetup<TestObject>()
            .Do(_ => _.CreateNewTableState())
            .Do(_ => _.WithEnumerableDataSource(data))
            .Do(_ => _.Sorting.SortByProperty(propertyName, ascending));
        
        // Act
        await setup
            .Do(_ => _.ReloadData());
        
        // Assert
        var first = setup.TableState.Items.Select(property).First();
        first.ShouldBe(ascending ? minimum : maximum);
    }
}