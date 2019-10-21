using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreWorkshop.Api
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ValidationException ex)
            {
                var error = new ValidationProblemDetails
                {
                    Detail = "See error messages for details.",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.HttpContext.Request.Path,
                    Title = "A bad request was received."
                };

                foreach (var err in ex.Errors.GroupBy(e => e.PropertyName))
                {
                    error.Errors.Add(err.Key, err.Select(e => e.ErrorMessage).ToArray());
                }

                context.Result = new BadRequestObjectResult(error)
                {
                    //stops default Content-Type of application/problem+json, which WebApi.Client has a hard time with out of the box!
                    ContentTypes = {"application/json"}
                };
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.ExceptionHandled = true;
            }

            return Task.CompletedTask;
        }
    }
}