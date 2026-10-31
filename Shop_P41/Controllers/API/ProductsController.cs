using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop_P41.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateAsync(product);
                return Ok(Json(result));
            }
            return BadRequest(new { error = "Parsing model ..." });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  // Авторизация с использованием схемы JWT Bearer
        [Authorize(Roles = "admin,moderator")]
        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(Json(products));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateAsync(id, product);
                return Ok(Json(result));
            }
            return BadRequest("Error ModelState!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _productService.DeleteAsync(id);
            return Ok(Json(result));
        }
    }
}
