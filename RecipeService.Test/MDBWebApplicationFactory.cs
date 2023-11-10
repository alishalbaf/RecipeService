using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeService.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Test
{
    public class MemoryWebApplicationFactory<T>:WebApplicationFactory<T>
        where T : class
    {
        private readonly string _dbName = Guid.NewGuid().ToString();
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault
                (d => d.ServiceType == typeof(DbContextOptions<RecipeDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<RecipeDbContext>(options =>
                {
                    // This is what makes a unique in-memory database per instance of TestWebApplicationFactory
                    options.UseInMemoryDatabase(_dbName);
                });
            });
        }
    }
}
