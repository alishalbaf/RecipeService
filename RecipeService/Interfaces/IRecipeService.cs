using RecipeService.DTO;
using RecipeService.DTO.Requests;

namespace RecipeService.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDto>> GetAllRecipeAsync(PageDto pageDto);
        Task<RecipeDto?> GetRecipeByIdAsync(int id);
        Task<RecipeDto> CreateRecipeAsync(RecipeCreateDto recipeDto);
        Task<bool> UpdateRecipeAsync(RecipeUpdateDto recipeDto);
        Task<bool> DeleteRecipeAsync(int id);
    }
}
