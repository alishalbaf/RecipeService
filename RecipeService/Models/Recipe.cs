
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Instructions { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}