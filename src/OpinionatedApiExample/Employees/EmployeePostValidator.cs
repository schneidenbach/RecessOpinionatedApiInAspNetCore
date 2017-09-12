using FluentValidation;
using OpinionatedApiExample.Shared;
using OpinionatedApiExample.Shared.Rest.CommandsAndHandlers;

namespace OpinionatedApiExample.Employees.Models
{
    public class EmployeePostValidator : OpinionatedValidator<RestPostRequest<Employee, EmployeePostModel, EmployeeModel>>
    {
        public EmployeePostValidator(OpinionatedDbContext opinionatedDbContext) 
            : base(opinionatedDbContext)
        {
            RuleFor(e => e.NewEntity.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(e => e.NewEntity.LastName).NotEmpty().WithMessage("Last name is required.");
        }
    }
}