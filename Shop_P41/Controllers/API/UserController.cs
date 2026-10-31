using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shop_P41.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop_P41.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ModelLogin modelLogin)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(modelLogin.Email);
                if(user == null)
                {
                    return BadRequest(new { error = $"User: {modelLogin.Email} not found!" });
                }
                var result = await _signInManager.PasswordSignInAsync(
                        user.UserName,
                        modelLogin.Password,
                        isPersistent: false,
                        lockoutOnFailure: false
                    );
                if(!result.Succeeded)
                {
                    return BadRequest(new { error = $"User: {modelLogin.Email} not authed!" });
                }
                var token = await GenerateJwtToken(user);
                return Ok(new
                {
                    token = token,
                    email = user.Email,
                    username = user.UserName
                });
            }
            return BadRequest(new { error = "Error model state!" });
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var user_roles = await _userManager.GetRolesAsync(user);
            var auth_claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            foreach (var role in user_roles)
            {
                auth_claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string my_token_password = "A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(my_token_password));
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5089",
                audience: "http://localhost:5089",
                expires: DateTime.UtcNow.AddMinutes(10),
                claims: auth_claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //Create method when creating jwt token
    }
}
