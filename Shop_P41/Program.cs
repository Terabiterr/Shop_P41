using Microsoft.EntityFrameworkCore;


namespace Shop_P41
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            var app = builder.Build();

            //Продовжити писати реалізацію моделей та робити міграції
            app.MapGet("/", () =>
            {

            });
            app.Run();
        }
    }
}
