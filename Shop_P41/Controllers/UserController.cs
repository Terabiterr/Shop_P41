using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_P41.Models;

namespace Shop_P41.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        // POST: http://localhost:[port]/user/register
        [HttpPost]
        public async Task<IActionResult> Register(ModelRegister model)
        {
            if(ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(newUser, model.Password);
                return Ok($"User: {model.Username} is registered succesfully ...");
            }
            return BadRequest($"error count: {ModelState.ErrorCount}");
        }
    }
}
