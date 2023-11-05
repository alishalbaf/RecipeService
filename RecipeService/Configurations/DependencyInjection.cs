using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RecipeService.DB;
using RecipeService.Interfaces;
using RecipeService.Mappings;

namespace RecipeService.Configurations
{
    public static class DependencyInjection
    {


        public static void AddInfrastructureApi(this IServiceCollection services,IConfiguration configuration)
        {
            var dbSelection = configuration.GetValue<string>("DBType") ?? "memory"; 
            var connectionString = configuration.GetValue<string>(dbSelection);

            
            services.AddDbContext<RecipeDbContext>(
                options =>
                {
                    switch (dbSelection)
                    {
                        case "mssql":
                            options.UseSqlServer(connectionString);
                            break;
                        case "sqlite":
                            options.UseSqlServer(connectionString);
                            break;
                        case "memory":
                        default:
                            options.UseInMemoryDatabase("Recipe");
                            break;
                    }
                }
            );
            services.AddScoped<IRecipeService, Services.RecipeService>();
            services.AddAutoMapper(typeof(DomainToDtoProfile));
        }
    }
}
