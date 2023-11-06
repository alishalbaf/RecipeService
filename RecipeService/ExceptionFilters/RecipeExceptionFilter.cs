using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RecipeService.Exceptions;
using RecipeService.NewFolder;
using System.Net;

namespace RecipeService.ExceptionFilters
{
    public class RecipeExceptionFilter : BaseExceptionFilter
    {
        private readonly ILogger<BaseExceptionFilter> logger;

        public RecipeExceptionFilter(ProblemDetailsFactory problemDetailsFactory, ILogger<BaseExceptionFilter> logger) : base(problemDetailsFactory)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError($"{context.Exception.Message} at {context.Exception.StackTrace}");
            //only catch Domain Exception
            //if (context.Exception is not DomainException) return;
            switch (context.Exception)
            {
                case KeyNotFoundException
                    or RecipeNotFoundException
                    or FileNotFoundException
                    or ArgumentNullException:
                    context.Result = GetProblemDetails(context, HttpStatusCode.NotFound);
                    break;
                case DuplicateRecipeException:
                    context.Result = GetProblemDetails(context, HttpStatusCode.Conflict);
                    break;
                case ArgumentException
                    or InvalidOperationException:
                    context.Result = GetProblemDetails(context, HttpStatusCode.BadRequest);

                    break;

                default:
                    context.Result=GetProblemDetails(context, HttpStatusCode.InternalServerError,"An error occured. ");
                    break;
            }

            return;
        }
    }
}
