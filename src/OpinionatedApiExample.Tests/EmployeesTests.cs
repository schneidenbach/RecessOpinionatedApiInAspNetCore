using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OpinionatedApiExample.Tests
{
    public class EmployeesTests : TestBase
    {
        [Test]
        public async Task EmployeeCreatedSuccessfully()
        {
            var id = await CreateNewSpencyAsync();

            Assert.That(id, Is.Not.Null);

            var resp = await Client.GetAsync("api/Employees/" + id.ToString());
            resp.EnsureSuccessStatusCode();
            var createdEmployee = await resp.Content.ReadAsAsync<dynamic>();
            
            Assert.That(createdEmployee.firstName?.ToString(), Is.EqualTo("Spencer"));
            Assert.That(createdEmployee.lastName?.ToString(), Is.EqualTo("Schneidenbach"));
        }
    }
}