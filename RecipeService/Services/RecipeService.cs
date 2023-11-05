using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeService.DB;
using RecipeService.DTO;
using RecipeService.DTO.Requests;
using RecipeService.Exceptions;
using RecipeService.Interfaces;
using RecipeService.Models;

namespace RecipeService.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext recipeDbContext;
        private readonly IMapper mapper;
        private readonly ILogger<RecipeService> logger;

        public RecipeService(RecipeDbContext recipeDbContext,IMapper mapper,ILogger<RecipeService> logger)
        {
            this.recipeDbContext = recipeDbContext;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<RecipeDto> CreateRecipeAsync(RecipeCreateDto recipeDto)
        {
            var recipe =mapper.Map<Recipe>(recipeDto);
            var addedRecipe=await recipeDbContext.AddAsync(recipe);
            
            
            await recipeDbContext.SaveChangesAsync();

            return mapper.Map<RecipeDto>(addedRecipe.Entity);
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            // this is a workaround because we are limited to .NET 6
            // if we have .NET 7  this would be an alternate soloution 
            //  recipeDbContext.Recipes.Where(c => c.Id == Id).ExecuteDeleteAsync();
            var recipe =new Recipe() { Id = id};
            
            recipeDbContext.Recipes.Remove(recipe);
            ///////////////////////////////////////////////////////
            int numOfAffected = await recipeDbContext.SaveChangesAsync();
            return numOfAffected>0;
        }

        public async Task<IEnumerable<RecipeDto>> GetAllRecipeAsync(int? startIndex,int? pageSize)
        {
            var listQuery = recipeDbContext.Recipes.Include(r=>r.Ingredients).AsQueryable();
            if (startIndex is not null) listQuery = listQuery.Skip(startIndex.Value);
            if (pageSize is not null) listQuery=listQuery.Take(pageSize.Value);

            var recipes= await listQuery.ToListAsync();
            return mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        public async Task<RecipeDto?> GetRecipeByIdAsync(int id)
        {
            var result=await recipeDbContext.Recipes.Include(r=>r.Ingredients).FirstOrDefaultAsync(r=>r.Id==id);
            
            return mapper.Map<RecipeDto>(result);
        }

        public async Task<bool> UpdateRecipeAsync(RecipeUpdateDto recipeUpdate)
        {
            var recipe = await recipeDbContext.Recipes.FindAsync(recipeUpdate.Id);
            if (recipe is null)
                throw new RecipeNotFoundException("");
            
            mapper.Map(recipeUpdate, recipe);
            recipeDbContext.Update(recipe);
            var numberOfiItemsAffected=await recipeDbContext.SaveChangesAsync();
            return numberOfiItemsAffected>0;

        }
    }
}
