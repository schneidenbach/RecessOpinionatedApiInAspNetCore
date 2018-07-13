using OpinionatedApiExample.Shared.Gets;

namespace OpinionatedApiExample.Employees
{
    public class EmployeeModel : IGetModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}