using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop_P41.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        // POST: http://localhost:[port]/user/register
        // Метод дії для обробки реєстрації користувача
        [HttpPost] 
        public async Task<IActionResult> Register(string email, string password)
        {
            // Перевірка на наявність email та пароля
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email або password are important ..."); // Повертає помилку, якщо дані не заповнені
            }

            // Створення нового користувача
            var user = new IdentityUser
            {
                UserName = email, // Встановлення імені користувача
                Email = email, // Встановлення email
                EmailConfirmed = true // Підтвердження email
            };

            // Створення користувача за допомогою UserManager
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded) // Якщо реєстрація пройшла успішно
            {
                return Ok("User is registered ..."); // Повертає повідомлення про успішну реєстрацію
            }

            // Висновок помилок, якщо реєстрація не вдалася
            foreach (var item in result.Errors)
            {
                Console.WriteLine(item); // Налагоджувальне повідомлення
            }
            return BadRequest(Json(result.Errors)); // Повертає помилки валідації
        }

    }
}
