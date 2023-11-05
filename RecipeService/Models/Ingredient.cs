using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Models
{
    [PrimaryKey(nameof(Name),nameof(RecipeId))]
    public class Ingredient
    {
        
        public string Name { get; set; }
        [Required]
        public decimal Value { get; set; }
        public string? Unit { get; set; }
   
        public int RecipeId { get; set; }
        public virtual Recipe Recipe {get;set;}
        
    }
}
