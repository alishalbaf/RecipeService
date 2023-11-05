using System.ComponentModel.DataAnnotations;
#pragma warning disable CS1591
namespace RecipeService.DTO
{
    public class RecipeDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Instructions is Required")]
        public string Instructions { get; set; }

        [Required(ErrorMessage = "The Ingredients is Required")]
        [MinLength(1)]
        public List<IngredientDto> Ingredients { get; set; }

    }
}
