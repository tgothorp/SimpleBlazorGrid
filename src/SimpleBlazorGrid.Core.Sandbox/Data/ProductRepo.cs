using Bogus;

namespace SimpleBlazorGrid.Sandbox.Data;

public class ProductRepo
{
    private List<Product> _products;

    public ProductRepo()
    {
        _products = new List<Product>();
        
        var faker = ProductFaker.Create();
        var products = faker.GenerateBetween(20, 85);

        // Introduce some nulls for test purposes
        products[0].SetNameToNull();
        
        _products.AddRange(products);
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }
}