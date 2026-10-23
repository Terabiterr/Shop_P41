using Microsoft.EntityFrameworkCore;

namespace Shop_P41.Services
{
    public interface IImageService
    {
        public Task<IEnumerable<ProductImage>> GetImagesAsync();
        public Task<ProductImage> InsertImageAsync(ProductImage productImage);
        public Task<ProductImage> UpdateImageAsync(int id, ProductImage productImage);
        public Task<ProductImage> DeleteImageAsync(int id);
    }
    public class ImageService : IImageService
    {
        private readonly ShopContext _context;
        public ImageService(ShopContext context)
        {
            _context = context;
        }

        public async Task<ProductImage> DeleteImageAsync(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if(productImage != null)
            {
                _context.ProductImages.Remove(productImage);
                await _context.SaveChangesAsync();
            }
            return productImage;
        }

        public async Task<IEnumerable<ProductImage>> GetImagesAsync()
            => await _context.ProductImages.ToListAsync();

        public async Task<ProductImage> InsertImageAsync(ProductImage productImage)
        {
            await _context.ProductImages.AddAsync(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }

        public async Task<ProductImage> UpdateImageAsync(int id, ProductImage productImage)
        {
            var productImageUpdate = await _context.ProductImages.FindAsync(id);
            if (productImage != null)
            {
                productImageUpdate.ImageUrl = productImage.ImageUrl;
                productImageUpdate.ProductId = productImage.ProductId;
                await _context.SaveChangesAsync();
            }
            return productImageUpdate;
        }
    }
}
