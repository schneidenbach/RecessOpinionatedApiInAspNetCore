using FluentValidation;
using OpinionatedApiExample.Employees;
using OpinionatedApiExample.Shared;
using OpinionatedApiExample.Shared.Rest.CommandsAndHandlers;

namespace OpinionatedApiExample.Jobs
{
    public class JobPostValidator : OpinionatedValidator<RestPostRequest<Job, JobPostModel, JobModel>>
    {
        public JobPostValidator(OpinionatedDbContext opinionatedDbContext) 
            : base(opinionatedDbContext)
        {
            RuleFor(e => e.NewEntity.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(e => e.NewEntity.Number).NotEmpty().WithMessage("Number is required.");
            RuleFor(e => e.NewEntity.ProjectManagerId).NotEmpty().WithMessage("ProjectManagerId is required.")
            .DependentRules(rules => {
                rules.RuleFor(e => e.NewEntity.ProjectManagerId).MustAsync(ExistAsync<Employee>)
                    .WithMessage("An existing Employee ID is required for ProjectManagerId.");
            });
        }
    }
}