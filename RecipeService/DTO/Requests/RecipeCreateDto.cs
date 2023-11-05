using System.ComponentModel.DataAnnotations;
#pragma warning disable CS1591
namespace RecipeService.DTO.Requests
{
    public class RecipeCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public List<IngredientDto> Ingredients { get; set; }

        [Required]
        public String Instructions { get; set; }

    }
}
