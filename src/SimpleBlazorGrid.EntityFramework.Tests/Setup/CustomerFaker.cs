using Bogus;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;

namespace SimpleBlazorGrid.EntityFramework.Tests.Setup;

public static class CustomerFaker
{
    public static Faker<Customer> Create()
    {
        return new Faker<Customer>()
            .RuleFor(prop => prop.CustomerId, val => val.Random.Guid().ToString())
            .RuleFor(prop => prop.CompanyName, val => val.Company.CompanyName())
            .RuleFor(prop => prop.ContactName, val => val.Person.FullName)
            .RuleFor(prop => prop.ContactTitle, val => "Mx")
            .RuleFor(prop => prop.Address, val => val.Address.StreetAddress())
            .RuleFor(prop => prop.City, val => val.Address.City())
            .RuleFor(prop => prop.Region, val => val.Address.State())
            .RuleFor(prop => prop.PostalCode, val => val.Address.ZipCode())
            .RuleFor(prop => prop.Country, val => val.Address.Country())
            .RuleFor(prop => prop.Phone, val => val.Phone.PhoneNumber())
            .RuleFor(prop => prop.Fax, val => val.Phone.PhoneNumber());
    }
}