using System.Diagnostics.CodeAnalysis;
using Shouldly;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;
using SimpleBlazorGrid.EntityFramework.Tests.Setup;

namespace SimpleBlazorGrid.EntityFramework.Tests.Filters;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
public class DateFilterTests
{
    [Fact]
    public async Task DateFilter_ExcludeTime_SameNullableDateTimeMatches()
    {
        var order = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleDateFilter<Order>
        {
            For = x => x.OrderDate,
            Value = order.OrderDate,
            IncludeTime = false
        };
        
        var source = TestSetup.CreateDataSource(context.Orders, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.OrderId == order.OrderId).ShouldNotBeNull();
    }
    
    [Fact]
    public async Task DateFilter_IncludeTime_SameNullableDateTimeMatches()
    {
        var order = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleDateFilter<Order>
        {
            For = x => x.OrderDate,
            Value = order.OrderDate,
            IncludeTime = false
        };
        
        var source = TestSetup.CreateDataSource(context.Orders, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.OrderId == order.OrderId).ShouldNotBeNull();
    }
    
    [Fact]
    public async Task DateFilter_IncludeTime_DateOnlyNullableDateTimeDoesNotMatch()
    {
        var order = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleDateFilter<Order>
        {
            For = x => x.OrderDate,
            Value = order.OrderDate!.Value.Date,
            IncludeTime = true
        };
        
        var source = TestSetup.CreateDataSource(context.Orders, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task DateFilter_ExcludeTime_NullableDateOnly_DoesMatch()
    {
        var order = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleDateFilter<Order>
        {
            For = x => x.LatestRefundDate,
            Value = new DateTime(order.LatestRefundDate!.Value.Year, order.LatestRefundDate.Value.Month, order.LatestRefundDate.Value.Day),
            IncludeTime = false
        };
        
        var source = TestSetup.CreateDataSource(context.Orders, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.OrderId == order.OrderId).ShouldNotBeNull();
    }
    
    [Fact]
    public async Task DateFilter_IncludeTime_NullableDateOnly_DoesMatch()
    {
        var order = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleDateFilter<Order>
        {
            For = x => x.LatestRefundDate,
            Value = new DateTime(order.LatestRefundDate!.Value.Year, order.LatestRefundDate.Value.Month, order.LatestRefundDate.Value.Day),
            IncludeTime = true
        };
        
        var source = TestSetup.CreateDataSource(context.Orders, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.OrderId == order.OrderId).ShouldNotBeNull();
    }
}