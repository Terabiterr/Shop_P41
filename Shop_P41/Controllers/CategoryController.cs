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

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            return View(category);
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost("{id}")]
        public IActionResult Update(int id, Category category)
        {
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
