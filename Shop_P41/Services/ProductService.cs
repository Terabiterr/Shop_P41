using Microsoft.EntityFrameworkCore;

public interface IProductService
{
    //Додати методи CRUD для продукту
    public Task<IEnumerable<Product>> GetProductsAsync();
    public Task<Product> GetProductByIdAsync(Guid id);
    public Task<Product> CreateAsync(Product product);
    public Task<Product> UpdateAsync(int id,  Product product);
    public Task<Product> DeleteAsync(int id);
}

public class ProductService : IProductService
{
    private readonly ShopContext _context;
    public ProductService(ShopContext context)
    {
        _context = context;
    }

    public Task<Product> CreateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetProducts()
        => await _context.Products.ToListAsync();

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateAsync(int id, Product product)
    {
        throw new NotImplementedException();
    }
}
