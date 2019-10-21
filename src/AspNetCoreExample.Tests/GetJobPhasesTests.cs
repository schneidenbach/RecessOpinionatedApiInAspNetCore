using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Api;
using AspNetCoreWorkshop.Api.JobPhases.GetJobPhasesForJob;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AspNetCoreWorkshop.Tests
{
    public class GetJobPhasesTests : TestBase
    {
        [Test]
        public async Task Get_job_phases_response_returns_all_data()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<WorkshopDbContext>();
                var job = context.Jobs.Single(j => j.Number == "George");
                
                var client = server.CreateClient();
                var resp = await client.GetAsync($"api/jobs/{job.Id}/phases");
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var json = await resp.Content.ReadAsAsync<IEnumerable<GetJobPhasesForJobResponse>>();
                Assert.That(json.Count, Is.EqualTo(3));
                Assert.That(json.Any(j => j.Number == "0046"));
            }
        }
    }
}