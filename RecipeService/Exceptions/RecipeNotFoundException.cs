namespace RecipeService.Exceptions
{
    public class RecipeNotFoundException:DomainException
    {
        public RecipeNotFoundException(string message):base(message) { }
    }
}
