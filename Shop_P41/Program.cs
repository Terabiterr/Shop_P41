using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop_P41.Services;
using System.Security.Claims;
using System.Text;


namespace Shop_P41
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            // =========================
            // IDENTITY (COOKIE AUTH)
            // =========================
            builder.Services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;

                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ShopContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "ShopApp.Auth";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.SlidingExpiration = true;

                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Denied";
            });

            // =========================
            // AUTHENTICATION (JWT)
            // =========================
            builder.Services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

                        RoleClaimType = ClaimTypes.Role
                    };
                });

            // =========================
            // AUTHORIZATION POLICY FOR API
            // =========================
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiPolicy", policy =>
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                          .RequireAuthenticatedUser());
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
