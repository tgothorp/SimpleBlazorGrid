using Bogus;

namespace SimpleBlazorGrid.Sandbox.Data;

public static class ProductFaker
{
    public static Faker<Product> Create()
    {
        return new Faker<Product>()
            .RuleFor(x => x.Id, v => v.Random.Int(1000, 99999))
            .RuleFor(x => x.Name, y => y.Commerce.Product())
            .RuleFor(x => x.Code, y => y.Random.String2(8))
            .RuleFor(x => x.InStockCount, y => y.Random.Int(0, 10))
            .RuleFor(x => x.LocalCurrency, y => y.PickRandom<Currency>())
            .RuleFor(x => x.CostInLocalCurrency, y => y.Finance.Amount(10))
            .RuleFor(x => x.Manufacturer, y => y.Company.CompanyName())
            .RuleFor(x => x.LastOrderedOn, y => y.Date.Between(DateTime.Today.AddDays(-14), DateTime.Today));
    }
}