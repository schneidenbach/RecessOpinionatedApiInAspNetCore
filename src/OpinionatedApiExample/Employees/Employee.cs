using OpinionatedApiExample.Shared;

namespace OpinionatedApiExample.Employees
{
    public class Employee : OpinionatedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
    }
}