using Microsoft.EntityFrameworkCore;
using RecipeService.Models;

namespace RecipeService.DB
{
    public class RecipeDbContext:DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(i=>i.Recipe );
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

    }
}
