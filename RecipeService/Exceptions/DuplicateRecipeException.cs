namespace RecipeService.Exceptions
{
    public class DuplicateRecipeException:DomainException
    {
        public DuplicateRecipeException(string message):base(message) { }
    }
}
