using AspNetCoreWorkshop.Api.Jobs.GetJobs;
using FluentValidation;

namespace AspNetCoreWorkshop.Api.Jobs.GetJob
{
    public class GetJobRequestValidator : AbstractValidator<GetJobRequest>
    {
        public GetJobRequestValidator()
        {
        }
    }
}