using Microsoft.AspNetCore.Mvc;
using Shop_P41.Services;

namespace Shop_P41.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var images = await _imageService.GetImagesAsync();
            return View(images);
        }
        [HttpGet]
        public IActionResult Insert() => View(new ProductImage { ImageUrl= "Default ImageUrl", ProductId=0 });
        [HttpPost]
        public async Task<IActionResult> Insert(ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                await _imageService.InsertImageAsync(productImage);
                return RedirectToAction(nameof(Index));
            }
            return View(productImage);
        }
    }
}
