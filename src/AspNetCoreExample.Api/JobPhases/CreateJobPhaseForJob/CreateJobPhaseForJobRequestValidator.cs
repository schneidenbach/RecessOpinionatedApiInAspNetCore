using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWorkshop.Api.JobPhases.CreateJobPhaseForJob
{
    public class CreateJobPhaseForJobRequestValidator : AbstractValidator<CreateJobPhaseForJobRequest>
    {
        public CreateJobPhaseForJobRequestValidator(WorkshopDbContext workshopDbContext)
        {
            WorkshopDbContext = workshopDbContext;

            RuleFor(r => r.Number).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Number must be a non-empty string.")
                .MustAsync(NotHaveExistingJobPhaseWithNumberAsync)
                .WithMessage("A job phase already exists with that number.");

            RuleFor(r => r.Description).NotEmpty().WithMessage("Description must be a non-empty string.");
        }

        public WorkshopDbContext WorkshopDbContext { get; }

        protected async Task<bool> NotHaveExistingJobPhaseWithNumberAsync(CreateJobPhaseForJobRequest request, string number, CancellationToken cancellationToken)
        {
            return !(await WorkshopDbContext.JobPhases.AnyAsync(p => p.Number == number && p.JobId == request.JobId, cancellationToken: cancellationToken));
        }
    }
}