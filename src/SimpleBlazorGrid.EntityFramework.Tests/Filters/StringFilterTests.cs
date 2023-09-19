using System.Diagnostics.CodeAnalysis;
using Shouldly;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;
using SimpleBlazorGrid.EntityFramework.Tests.Setup;

namespace SimpleBlazorGrid.EntityFramework.Tests.Filters;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
public class StringFilterTests
{
    [Fact]
    public async Task StringFilter_IgnoreCase_SameCaseMatches()
    {
        var customer = await DatabaseSetup.AddCustomer();
        var context = new DatabaseContext(true);

        var filter = new SimpleStringFilter<Customer>
        {
            For = x => x.CustomerId,
            IgnoreCase = true,
            Value = customer.CustomerId
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.CustomerId == customer.CustomerId).ShouldNotBeNull();
    }

    [Fact]
    public async Task StringFilter_IgnoreCase_DifferentCaseMatches()
    {
        var customer = await DatabaseSetup.AddCustomer();
        var context = new DatabaseContext(true);

        var filter = new SimpleStringFilter<Customer>
        {
            For = x => x.CustomerId,
            IgnoreCase = true,
            Value = customer.CustomerId.ToUpper()
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.CustomerId == customer.CustomerId).ShouldNotBeNull();
    }
    
    [Fact]
    public async Task StringFilter_MatchCase_SameCaseMatches()
    {
        var customer = await DatabaseSetup.AddCustomer();
        var context = new DatabaseContext(true);

        var filter = new SimpleStringFilter<Customer>
        {
            For = x => x.CustomerId,
            IgnoreCase = false,
            Value = customer.CustomerId
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.CustomerId == customer.CustomerId).ShouldNotBeNull();
    }

    [Fact]
    public async Task StringFilter_MatchCase_DifferentCaseDoesNotMatch()
    {
        var customer = await DatabaseSetup.AddCustomer();
        var context = new DatabaseContext(true);

        var filter = new SimpleStringFilter<Customer>
        {
            For = x => x.CustomerId,
            IgnoreCase = false,
            Value = customer.CustomerId.ToUpper()
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldBeEmpty();
    }
}