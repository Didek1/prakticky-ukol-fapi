using Eshop.Api.Interfaces;
using Eshop.Api.Managers;
using Eshop.Data;
using Eshop.Data.Interfaces;
using Eshop.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();

            // Registrace závislostí pro repositaře a manažery
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookManager, BookManager>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderManager, OrderManager>();

            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IOrderItemManager, OrderItemManager>();

            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IAddressManager, AddressManager>();

            builder.Services.AddAutoMapper(cfg => { }, typeof(AutomapperConfigurationProfile).Assembly);

            var app = builder.Build();

            app.MapControllers();

            // Vytvořeni endpointu pro získání kurzů ČNB
            app.MapGet("/api/cnb-rates", async () =>
            {
                using (var client = new HttpClient())
                {
                    string url = "https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt";

                    var res = await client.GetAsync(url);
                    if (!res.IsSuccessStatusCode)
                    {
                        return Results.Problem($"{res.StatusCode}");
                    }

                    var content = await res.Content.ReadAsStringAsync();
                    return Results.Text(content, "text/plain");
                }
            });

            app.Run();
        }
    }
}
