using System.ComponentModel.DataAnnotations;

namespace RecipeService.DTO
{
    public record IngredientDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Value { get; set; }
        public string? Unit { get; set; }
    }
}
