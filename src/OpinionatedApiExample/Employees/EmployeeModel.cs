using AutoMapper.Attributes;
using OpinionatedApiExample.Shared.Rest;

namespace OpinionatedApiExample.Employees.Models
{
    [MapsFrom(typeof(Employee))]
    public class EmployeeModel : IGetModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}