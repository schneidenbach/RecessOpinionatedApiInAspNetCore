using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AspNetCoreWorkshop.Tests
{
    public class CreateJobPhaseForJobTests : TestBase
    {
        [Test]
        public async Task Create_job_phase_for_job_succeeds_with_valid_request()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<WorkshopDbContext>();
                var job = context.Jobs.Single(j => j.Number == "George");
                
                var client = server.CreateClient();
                var resp = await client.PostAsJsonAsync($"api/jobs/{job.Id}/phases", new {
                    description = "this is a test description",
                    number = "3345"
                });
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK), await resp.Content.ReadAsStringAsync());
                Assert.That(context.JobPhases.Any(p => p.Number == "3345" && p.JobId == job.Id));
            }
        }
        
        [Test]
        public async Task Create_job_phase_for_job_returns_400_when_no_number_and_description_specified()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<WorkshopDbContext>();
                var job = context.Jobs.Single(j => j.Number == "George");
                
                var client = server.CreateClient();
                var resp = await client.PostAsJsonAsync($"api/jobs/{job.Id}/phases", new {});
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

                var contentJson = await resp.Content.ReadAsAsync<ValidationProblemDetails>();
                Assert.That(contentJson, Is.Not.Null);
                Assert.That(contentJson.Errors.ContainsKey("Number"), Is.True);
                Assert.That(contentJson.Errors.ContainsKey("Description"), Is.True);
            }
        }
        
        [Test]
        public async Task Create_job_phase_for_job_returns_400_when_number_exists_on_phase_for_job()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<WorkshopDbContext>();
                var job = context.Jobs.Single(j => j.Number == "George");
                
                var client = server.CreateClient();
                var resp = await client.PostAsJsonAsync($"api/jobs/{job.Id}/phases", new
                {
                    Description = "fail1",
                    Number = "0046"
                });
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

                var contentJson = await resp.Content.ReadAsAsync<ValidationProblemDetails>();
                Assert.That(contentJson, Is.Not.Null);
                Assert.That(contentJson.Errors.ContainsKey("Number"), Is.True);
            }
        }
    }
}