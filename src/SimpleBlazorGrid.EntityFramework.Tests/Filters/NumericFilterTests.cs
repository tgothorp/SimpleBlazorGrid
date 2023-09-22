using System.Diagnostics.CodeAnalysis;
using Shouldly;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;
using SimpleBlazorGrid.EntityFramework.Tests.Setup;

namespace SimpleBlazorGrid.EntityFramework.Tests.Filters;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
public class NumericFilterTests
{
    [Fact]
    public async Task NumericFilter_Int()
    {
        var order = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleNumericFilter<Order>
        {
            For = x => x.OrderId,
            Value = order.OrderId.ToString(),
        };

        var source = TestSetup.CreateDataSource(context.Orders, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.OrderId == order.OrderId).ShouldNotBeNull();
    }
    
    [Fact]
    public async Task NumericFilter_Decimal()
    {
        var order = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleNumericFilter<Order>
        {
            For = x => x.Freight,
            Value = order.Freight.ToString(),
        };

        var source = TestSetup.CreateDataSource(context.Orders, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.OrderId == order.OrderId).ShouldNotBeNull();
    }
}