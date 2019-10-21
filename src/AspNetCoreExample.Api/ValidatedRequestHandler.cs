using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace AspNetCoreWorkshop.Api
{
    public abstract class ValidatedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        protected ValidatedRequestHandler(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            // all request validators will run through here first before moving onto the OnHandle request.
            if (_validators != null)
            {
                var result = (await Task
                        .WhenAll(_validators.Where(v => v != null).Select(v => v.ValidateAsync(request, cancellationToken)))
                        .ConfigureAwait(false))
                    .SelectMany(v => v.Errors);

                if (result.Any())
                {
                    throw new ValidationException("no results", result);
                }
            }

            return await OnHandle(request, cancellationToken).ConfigureAwait(false);
        }

        protected abstract Task<TResponse> OnHandle(TRequest message, CancellationToken cancellationToken);
    }
}