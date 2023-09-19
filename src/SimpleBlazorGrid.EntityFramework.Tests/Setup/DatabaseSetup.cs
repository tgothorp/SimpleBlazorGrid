using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;

namespace SimpleBlazorGrid.EntityFramework.Tests.Setup;

public static class DatabaseSetup
{
    public static async Task<Customer> AddCustomer(Customer? customer = null)
    {
        customer = customer ?? CustomerFaker.Create();

        using (var context = new DatabaseContext(true))
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }

        return customer;
    }

    public static async Task<Order> AddOrder(Order? order = null, Customer? customer = null)
    {
        customer = customer ?? CustomerFaker.Create();
        order = order ?? OrderFaker.Create();

        order.Customer = customer;
        order.CustomerId = customer.CustomerId;

        using (var context = new DatabaseContext(true))
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        return order;
    }
}