using Bogus;

namespace SimpleBlazorGrid.Sandbox.Data;

public class ProductRepo
{
    private List<Product> _products;

    public ProductRepo()
    {
        _products = new List<Product>();
        
        var faker = ProductFaker.Create();
        _products.AddRange(faker.GenerateBetween(20, 85));
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }
}