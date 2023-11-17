using SimpleBlazorGrid.Core.Tests.Common.Dto;
using SimpleBlazorGrid.Core.Tests.Common.Extensions;
using SimpleBlazorGrid.Core.Tests.Common.TableState;
using SimpleBlazorGrid.Exceptions;

namespace SimpleBlazorGrid.Core.Tests.Unit.EnumerableDataSource;

public class SearchTests
{
    [Fact]
    public async Task SearchProperties_PropertyIsNotAString_Throws()
    {
        // Arrange
        var data = TestObjectFactory.CreateMany();

        var setup = await new TableStateSetup<TestObject>()
            .Do(_ => _.CreateNewTableState())
            .Do(_ => _.WithEnumerableDataSource(data))
            .Do(_ => _.Searching.WithSearchableColumns(nameof(TestObject.StringProperty), nameof(TestObject.DecimalProperty)))
            .Do(_ => _.Searching.WithSearchQuery("searchQuery"));
        
        // Act
        var exception = await Should.ThrowAsync<SimpleGridException>(async () =>
        {
            await setup
                .Do(_ => _.ReloadData());
        });

        // Await
        exception.Message.ShouldBe("Cannot apply search to property x.DecimalProperty as the target property is not a string!");
    }

    [Fact]
    public async Task SearchProperties_SingleTargetPropertyIsNull_DoesNotThrow()
    {
        // Arrange
        var data = TestObjectFactory.CreateMany(instantiateNullableProperties:false);
        
        var setup = await new TableStateSetup<TestObject>()
            .Do(_ => _.CreateNewTableState())
            .Do(_ => _.WithEnumerableDataSource(data))
            .Do(_ => _.Searching.WithSearchableColumns(nameof(TestObject.NullableStringProperty)))
            .Do(_ => _.Searching.WithSearchQuery("shouldMatchNothing"));
        
        // Act
        await setup
            .Do(_ => _.ReloadData());
        
        // Assert
        setup.TableState.Items.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task SearchProperties_SingleTargetPropertyMatchesExactly_ReturnsExpectedResults()
    {
        // Arrange
        var data = TestObjectFactory.CreateManyIdentical();
        var searchQuery = data[0].StringProperty;
        
        var setup = await new TableStateSetup<TestObject>()
            .Do(_ => _.CreateNewTableState())
            .Do(_ => _.WithEnumerableDataSource(data))
            .Do(_ => _.Searching.WithSearchableColumns(nameof(TestObject.StringProperty)))
            .Do(_ => _.Searching.WithSearchQuery(searchQuery));
        
        // Act
        await setup
            .Do(_ => _.ReloadData());
        
        // Assert
        setup.TableState.Items.ShouldNotBeEmpty();
        setup.TableState.TotalItemCount.ShouldBe(data.Count);
    }
    
    [Fact]
    public async Task SearchProperties_SingleTargetPropertyMatchesPartially_ReturnsExpectedResults()
    {
        // Arrange
        var data = TestObjectFactory.CreateManyIdentical();
        var searchQuery = $"{data[0].StringProperty[2]}{data[0].StringProperty[3]}{data[0].StringProperty[4]}";
        
        var setup = await new TableStateSetup<TestObject>()
            .Do(_ => _.CreateNewTableState())
            .Do(_ => _.WithEnumerableDataSource(data))
            .Do(_ => _.Searching.WithSearchableColumns(nameof(TestObject.StringProperty)))
            .Do(_ => _.Searching.WithSearchQuery(searchQuery));
        
        // Act
        await setup
            .Do(_ => _.ReloadData());
        
        // Assert
        setup.TableState.Items.ShouldNotBeEmpty();
        setup.TableState.TotalItemCount.ShouldBe(data.Count);
    }
    
    [Fact]
    public async Task SearchProperties_MultipleTargetPropertyMatchesExactly_ReturnsExpectedResults()
    {
        // Arrange
        var data = TestObjectFactory.CreateManyIdentical();
        var searchQuery = data[0].StringProperty;
        
        var setup = await new TableStateSetup<TestObject>()
            .Do(_ => _.CreateNewTableState())
            .Do(_ => _.WithEnumerableDataSource(data))
            .Do(_ => _.Searching.WithSearchableColumns(nameof(TestObject.StringProperty), nameof(TestObject.NullableStringProperty)))
            .Do(_ => _.Searching.WithSearchQuery(searchQuery));
        
        // Act
        await setup
            .Do(_ => _.ReloadData());
        
        // Assert
        setup.TableState.Items.ShouldNotBeEmpty();
        setup.TableState.TotalItemCount.ShouldBe(data.Count);
    }
    
    [Fact]
    public async Task SearchProperties_MultipleTargetPropertyMatchesPartially_ReturnsExpectedResults()
    {
        // Arrange
        var data = TestObjectFactory.CreateManyIdentical();
        var searchQuery = $"{data[0].StringProperty[2]}{data[0].StringProperty[3]}{data[0].StringProperty[4]}";
        
        var setup = await new TableStateSetup<TestObject>()
            .Do(_ => _.CreateNewTableState())
            .Do(_ => _.WithEnumerableDataSource(data))
            .Do(_ => _.Searching.WithSearchableColumns(nameof(TestObject.StringProperty), nameof(TestObject.NullableStringProperty)))
            .Do(_ => _.Searching.WithSearchQuery(searchQuery));
        
        // Act
        await setup
            .Do(_ => _.ReloadData());
        
        // Assert
        setup.TableState.Items.ShouldNotBeEmpty();
        setup.TableState.TotalItemCount.ShouldBe(data.Count);
    }
}