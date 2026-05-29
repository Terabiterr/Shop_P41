using Microsoft.EntityFrameworkCore;

public interface IProductService
{
    //Додати методи CRUD для продукту
    public Task<IEnumerable<Product>> GetProductsAsync();
    public Task<Product> GetProductByIdAsync(int id);
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

    public async Task<Product> CreateAsync(Product product)
    {
        if(product !=  null)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        throw new Exception("Error create product");
    }

    public async Task<Product> DeleteAsync(int id)
    {
        var product  = await _context.Products.FindAsync(id);
        if(product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
        throw new Exception("Error delete product");
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if(product != null)
        {
            return product;
        }
        throw new Exception("Product not found");
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
        => await _context.Products.ToListAsync();

    public async Task<Product> UpdateAsync(int id, Product product)
    {
        var existingProduct = await _context.Products.FindAsync(id);
        if(existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.Quantity = product.Quantity;
            existingProduct.CategoryId = product.CategoryId;
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }
        throw new Exception("Error update product");
    }
}
