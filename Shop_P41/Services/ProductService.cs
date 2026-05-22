public interface IProductService
{
    //Додати методи CRUD для продукту
    public IEnumerable<Product> GetProducts();
}

public class ProductService : IProductService
{
    private readonly ShopContext _context;
    public ProductService(ShopContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetProducts()
    {
        throw new NotImplementedException();
    }
}
