using Bogus;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;

namespace SimpleBlazorGrid.EntityFramework.Tests.Setup;

public static class OrderFaker
{
    public static Faker<Order> Create()
    {
        return new Faker<Order>()
            .RuleFor(prop => prop.OrderId, val => val.Random.Int(min: 0))
            .RuleFor(prop => prop.OrderDate, val => val.Date.Recent())
            .RuleFor(prop => prop.RequiredDate, val => val.Date.Soon())
            .RuleFor(prop => prop.ShippedDate, val => null)
            .RuleFor(prop => prop.LatestRefundDate, val => val.Date.FutureDateOnly())
            .RuleFor(prop => prop.Freight, val => Math.Round(val.Random.Int() + val.Random.Decimal(), 2));
    }
    
}