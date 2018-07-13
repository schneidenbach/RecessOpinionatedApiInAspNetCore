using AutoMapper;
using MediatR;
using OpinionatedApiExample.Shared;
using OpinionatedApiExample.Shared.Rest;

namespace OpinionatedApiExample.Employees
{
    public class EmployeesController : OpinionatedRestController<Employee, EmployeeModel, EmployeeModel, CreateEmployeeRequest>
    {
        public EmployeesController(OpinionatedDbContext opinionatedDbContext, IMapper mapper, IMediator mediator)
            : base(opinionatedDbContext, mapper, mediator)
        {
        }
    }
}
