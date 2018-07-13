using System;
using OpinionatedApiExample.Employees;
using OpinionatedApiExample.Shared.Gets;
using OpinionatedApiExample.Shared.Rest;

namespace OpinionatedApiExample.Jobs
{
    public class JobModel : IGetModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public EmployeeModel ProjectManager { get; set; }
    }
}