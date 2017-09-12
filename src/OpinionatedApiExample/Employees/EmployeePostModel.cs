using AutoMapper.Attributes;

namespace OpinionatedApiExample.Employees.Models
{
    [MapsTo(typeof(Employee))]
    public class EmployeePostModel : EmployeeModel
    {
        public string SocialSecurityNumber { get; set; }
    }
}