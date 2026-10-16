using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop_P41.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //GET METHOD ADD IMAGE TO PRODUCT
        [Authorize(Roles="Moderator")]
        public async Task<IActionResult> Image() => View();

        //POST METHOD ADD IMAGE TO PRODUCT
        [HttpPost]
        public async Task<IActionResult> Image(ProductImage  productImage)
        {
            Console.WriteLine($"Id: {productImage.Id}, Image url: {productImage.ImageUrl}, ProductId: {productImage.ProductId}");
            if (ModelState.IsValid)
            {
                //Додати до сервісу ProductService
                //method: AddImage(productImage)
                //Домашнє завдання:
                //Знайти помилку чому не додається фото до бази даних, та додати ще функціонал редагування та видалення фото
                await _productService.AddImage(productImage);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            return View(products);
        }
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if(ModelState.IsValid)
            {
                await _productService.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                _ = await _productService.UpdateAsync(product.Id, product);
                return RedirectToAction($"{nameof(Index)}");
            }
            return View(product);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            _ = await _productService.DeleteAsync(id);
            return RedirectToAction($"{nameof(Index)}");
        }
    }
}
