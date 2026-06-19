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
        public async Task<IActionResult> Read()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
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
                    return RedirectToAction("Read");
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
            return RedirectToAction("Read");
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryService.UpdateAsync(category.Id, category);
                    return RedirectToAction("Read");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                //Add logs
                Console.WriteLine(ex.Message);
            }
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return RedirectToAction("Read");
            }
            catch (Exception ex)
            {
                //Add logs
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Read");
        }
    }
}
