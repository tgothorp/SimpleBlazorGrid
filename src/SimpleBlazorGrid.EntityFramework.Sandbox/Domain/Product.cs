namespace SimpleBlazorGrid.EntityFramework.Sandbox.Domain;

public class Product
{
    public Product()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string QuantityPerUnit { get; set; } = null!;
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; private set; }
}