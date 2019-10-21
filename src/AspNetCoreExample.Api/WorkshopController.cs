using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWorkshop.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public abstract class WorkshopController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected WorkshopController(
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task<IActionResult> HandleRequestAsync<TReturn>(IRequest<TReturn> request)
        {
            if (request == null)
            {
                var error = new ValidationProblemDetails
                {
                    Detail = "The body of the request contained no usable content.",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = HttpContext.Request.Path,
                    Title = "A bad request was received."
                };

                return BadRequest(error);
            }

            var response = await _mediator.Send(request);
            if (response is IActionResult result)
            {
                return result;
            }

            return Ok(response);
        }
    }
}