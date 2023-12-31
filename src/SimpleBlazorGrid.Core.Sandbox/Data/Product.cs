namespace SimpleBlazorGrid.Sandbox.Data;

public class Product
{
    public long Id { get; protected set; }
    public bool InStock { get; protected set; }
    public string? Name { get; protected set; }
    public string? Code { get; protected set; }
    public int InStockCount { get; protected set; }
    public decimal CostInLocalCurrency { get; protected set; }
    public Currency LocalCurrency { get; protected set; }
    public string? Manufacturer { get; protected set; }
    public DateTime LastOrderedOn { get; protected set; }

    public decimal CostInGbp =>
        LocalCurrency switch
        {
            Currency.GBP => CostInLocalCurrency,
            Currency.USD => decimal.Round(CostInLocalCurrency / 1.5m, 2),
            Currency.EUR => decimal.Round(CostInLocalCurrency / 1.17m, 2),
            _ => throw new ArgumentOutOfRangeException()
        };

    public void SetNameToNull()
    {
        Name = null;
    }
}