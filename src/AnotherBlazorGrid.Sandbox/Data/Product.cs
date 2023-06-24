namespace AnotherBlazorGrid.Sandbox.Data;

public class Product
{
    public long Id { get; protected set; }
    public string Name { get; protected set; }
    public string Code { get; protected set; }
    public int InStockCount { get; protected set; }
    public decimal CostInLocalCurrency { get; protected set; }
    public Currency LocalCurrency { get; protected set; }

    public decimal CostInGbp =>
        LocalCurrency switch
        {
            Currency.GBP => CostInLocalCurrency,
            Currency.USD => CostInLocalCurrency / 1.5m,
            Currency.EUR => CostInLocalCurrency / 1.17m,
            _ => throw new ArgumentOutOfRangeException()
        };
}