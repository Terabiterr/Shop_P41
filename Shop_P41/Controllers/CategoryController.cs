using Microsoft.AspNetCore.Mvc;
using Shop_P41.Services;

namespace Shop_P41.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryService.CreateAsync(category);
                    return RedirectToAction("Index");
                } 
                else
                {
                    throw new Exception("Model not valid!!!");
                }
            }
            catch (Exception ex)
            {
                //Add logs
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryService.UpdateAsync(id, category);
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Model not valid!!!");
                }
            }
            catch (Exception ex)
            {
                //Add logs
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //Add logs
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
