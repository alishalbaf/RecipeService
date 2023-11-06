using Microsoft.AspNetCore.Mvc;
using RecipeService.DTO;
using RecipeService.DTO.Requests;
using RecipeService.Interfaces;

namespace RecipeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {


        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeService recipeService;

        public RecipeController(ILogger<RecipeController> logger,IRecipeService recipeService)
        {
            _logger = logger;
            this.recipeService = recipeService;
        }

        /// <summary>
        ///  Gets All The recipes
        ///  
        /// </summary>
        /// <returns> List of Recipes </returns>
        /// <response code="200">Returns a list of recipes</response>
        /// <response code="404">If there is no recipes</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> Get(int? start=null,int? pageSize= null)
        {
            _logger?.LogInformation("Returnnig all Recipes");
            var result = await recipeService.GetAllRecipeAsync(start,pageSize);
            if (result.Any())
                return Ok(result);
            return NotFound();
        }

        /// <summary>
        ///     Get Specified Recipe by id
        /// </summary>
        /// <param name="Id"> Id of recipe</param>
        /// <returns>single Recipe </returns>
        /// <response code="200">Returns a specifiec recipe</response>
        /// <response code="404">If the recipe is null</response>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> Get(int Id)
        {
            _logger?.LogInformation("Returnnig all Recipes");
            var result = await recipeService.GetRecipeByIdAsync(Id);
            if (result!=null)
                return Ok(result);
            return NotFound();
        }

        /// <summary>
        /// Create a recipe
        /// </summary>
        /// <param name="recipe"> Recipe </param>
        /// <returns>Id of newly created Recipe and whole object</returns>
        /// <response code="201">Returns the newly created recipe</response>
        /// <response code="400">If the recipe is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  async Task<ActionResult> Post([FromBody]RecipeCreateDto recipe)
        {
            _logger.LogInformation("Creating recipe");
            var createdDto = await recipeService.CreateRecipeAsync(recipe);
            return CreatedAtAction(nameof(Get), new { id = createdDto.Id }, createdDto);
        }
        /// <summary>
        ///     Update the whole or part of Recipe
        ///     fill the parts need to updated
        ///     remains should be null
        /// </summary>
        /// <param name="Id">Id of recipe to be updated</param>
        /// <param name="recipe"> Recipe to update</param>
        /// <returns>if updated return NoContent , if not found return NotFound</returns>
        /// <response code="204">if the Recipe is successfully updated</response>
        /// <response code="404">If the recipe is not found for update</response>
        [HttpPut("{Id}")]  //RFC 7230 to 7235 - 7231 section 4.3.4
        [HttpPatch("{Id}")] //partial Update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int Id,[FromBody] RecipeUpdateDto recipe)
        {
            _logger.LogInformation($"Updateing Recipe #{recipe.Id}");
            //workaround the bug that FromBody overwrite FromQuery in the model
            recipe.Id = Id;
            var result=await recipeService.UpdateRecipeAsync(recipe);
            if (result)
                return NoContent();
            return NotFound();
        }

        /// <summary>
        ///  Deelte specified Recipe
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>if Deleted return NoContent , if not found return NotFound</returns>
        /// <response code="204">if the Recipe is successfully deleted</response>
        /// <response code="404">If the recipe is not found for delete</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int Id)
        {
            _logger.LogInformation($"Deleting Recipe #{Id}");
            var result=await recipeService.DeleteRecipeAsync(Id);
            if (result)
                return NoContent();
            return NotFound(Id);
        }
    }
}