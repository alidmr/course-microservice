using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Linq;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FreeCourse.Services.Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
                if (!categoryService.GetAllAsync().Result.Data.Any())
                {
                    categoryService.CreateAsync(new CategoryDto()
                    {
                        Name = "Asp.Net Core"
                    }).Wait();
                    categoryService.CreateAsync(new CategoryDto()
                    {
                        Name = "Sql Server"
                    }).Wait();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}