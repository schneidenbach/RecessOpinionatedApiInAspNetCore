using FluentValidation;

namespace AspNetCoreWorkshop.Api.Jobs.CreateJob
{
    public class CreateJobRequestValidator : AbstractValidator<CreateJobRequest>
    {
        public CreateJobRequestValidator()
        {
            RuleFor(r => r.Number).NotEmpty().WithMessage("Please enter a number.");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Please enter a name.");
            RuleFor(r => r.StartDate).NotEmpty().WithMessage("Please enter a start date.");
        }
    }
}