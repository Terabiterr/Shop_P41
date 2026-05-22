using Microsoft.EntityFrameworkCore;

namespace Shop_P41
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(@"");
            });
            var app = builder.Build();

            

            app.Run();
        }
    }
}
