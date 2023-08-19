namespace SimpleBlazorGrid.EntityFramework.Sandbox.Domain;

public class Customer
{
    public Customer()
    {
        Orders = new HashSet<Order>();
    }

    public string CustomerId { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string ContactTitle { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Fax { get; set; } = null!;

    public ICollection<Order> Orders { get; private set; }
}