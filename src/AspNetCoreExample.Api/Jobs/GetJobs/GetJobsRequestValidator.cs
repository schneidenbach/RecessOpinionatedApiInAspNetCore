using System.Linq;
using FluentValidation;

namespace AspNetCoreWorkshop.Api.Jobs.GetJobs
{
    public class GetJobsRequestValidator : AbstractValidator<GetJobsRequest>
    {
        public GetJobsRequestValidator()
        {
            RuleFor(r => r.OrderBy).Must(BeAValidProperyName)
                .WithMessage("OrderBy parameter not valid.");
        }

        public bool BeAValidProperyName(string propName)
        {
            if (string.IsNullOrWhiteSpace(propName))
            {
                return true;
            }

            var validPropNames = typeof(GetJobsResponse)
                .GetProperties().Select(p => p.Name);
            return validPropNames.Contains(propName);
        }
    }
}