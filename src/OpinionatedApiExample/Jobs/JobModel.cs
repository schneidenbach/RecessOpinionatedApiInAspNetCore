using System;
using AutoMapper.Attributes;
using OpinionatedApiExample.Employees.Models;
using OpinionatedApiExample.Shared.Rest;

namespace OpinionatedApiExample.Jobs
{
    [MapsFrom(typeof(Job))]
    public class JobModel : IGetModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public EmployeeModel ProjectManager { get; set; }
    }
}