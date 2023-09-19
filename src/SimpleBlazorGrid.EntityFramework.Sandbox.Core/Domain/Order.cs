namespace SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;

public class Order
{
    public Order()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    public int OrderId { get; set; }
    public string CustomerId { get; set; } = null!;
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateOnly? LatestRefundDate { get; set; }
    public decimal? Freight { get; set; }

    public Customer Customer { get; set; } = null!;
    public ICollection<OrderDetail> OrderDetails { get; private set; }
}