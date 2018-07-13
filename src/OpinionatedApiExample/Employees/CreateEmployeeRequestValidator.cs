using FluentValidation;
using OpinionatedApiExample.Shared;
using OpinionatedApiExample.Shared.Rest;

namespace OpinionatedApiExample.Employees
{
    public class CreateEmployeeRequestValidator : OpinionatedValidator<RestPostRequest<Employee, CreateEmployeeRequest, EmployeeModel>>
    {
        public CreateEmployeeRequestValidator(OpinionatedDbContext opinionatedDbContext) 
            : base(opinionatedDbContext)
        {
            RuleFor(e => e.NewEntity.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(e => e.NewEntity.LastName).NotEmpty().WithMessage("Last name is required.");
        }
    }
}