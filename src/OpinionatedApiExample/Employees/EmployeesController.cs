using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpinionatedApiExample.Employees;
using OpinionatedApiExample.Employees.Models;
using OpinionatedApiExample.Shared;
using OpinionatedApiExample.Shared.Rest;

namespace OpinionatedApiExample.Controllers
{
    public class EmployeesController : OpinionatedRestController<Employee, EmployeeModel, EmployeeModel, EmployeePostModel>
    {
        public EmployeesController(OpinionatedDbContext opinionatedDbContext, IMapper mapper, IMediator mediator)
            : base(opinionatedDbContext, mapper, mediator)
        {
        }
    }
}
