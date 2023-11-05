using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RecipeService.NewFolder
{
    public abstract class BaseExceptionFilter : IExceptionFilter
    {
        private readonly ProblemDetailsFactory problemDetailsFactory;

        public BaseExceptionFilter(ProblemDetailsFactory problemDetailsFactory)
        {
            this.problemDetailsFactory = problemDetailsFactory;
        }

        protected IActionResult GetProblemDetails(ExceptionContext context, HttpStatusCode httpStatusCode, string? message = null)
        {
            var problemDetails = problemDetailsFactory.CreateProblemDetails
                    (context.HttpContext, (int)httpStatusCode, message ?? context.Exception.Message);

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };

        }
        public abstract void OnException(ExceptionContext context);
        
        
    }
}
