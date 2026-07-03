using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_P41.Models;

namespace Shop_P41.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(ModelLogin model)
        {
            if (ModelState.IsValid)
            {
                // 1. Ищем пользователя в таблице по Email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(
                        user.UserName!,
                        model.Password,
                        isPersistent: true,
                        lockoutOnFailure: false
                    );

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }

                return BadRequest("Error: Invalid login attempt.");
            }

            return BadRequest($"error count: {ModelState.ErrorCount}");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }
    }
}
