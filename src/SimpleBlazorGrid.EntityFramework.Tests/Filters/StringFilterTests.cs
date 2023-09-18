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
    public async Task StringFilter()
    {
        var context = new DatabaseContext();
        await SeedDatabase(context);
        
        var filter = new SimpleStringFilter<Customer>()
        {
            For = x => x.ContactName,
            IgnoreCase = true,
            Value = "ABC"
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);

        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
    }

    private async Task SeedDatabase(DatabaseContext context)
    {
        var customer = new Customer
        {
            CustomerId = Guid.NewGuid().ToString(),
            CompanyName = "ABC",
            ContactName = "ABC",
            ContactTitle = "ABC",
            Address = "ABC",
            City = "ABC",
            Region = "ABC",
            PostalCode = "ABC",
            Country = "ABC",
            Fax = "ABC",
            Phone = "ABC",
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();
    }
}