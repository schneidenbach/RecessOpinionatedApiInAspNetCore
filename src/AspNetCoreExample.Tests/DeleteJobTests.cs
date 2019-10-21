using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Api;
using AspNetCoreWorkshop.Api.Jobs.GetJob;
using AspNetCoreWorkshop.Api.Jobs.GetJobs;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AspNetCoreWorkshop.Tests
{
    public class DeleteJobTests : TestBase
    {
        private const string JobsUrl = "/api/jobs";

        [Test]
        public async Task Delete_jobs_response_returns_not_found_when_job_does_not_exist()
        {
            using (var server = CreateTestServer())
            {
                var client = server.CreateClient();
                var resp = await client.GetAsync(JobsUrl + "/100000");
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public async Task Delete_job_response_deletes_record_from_database()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<WorkshopDbContext>();
                var job = context.Jobs.First();
                
                var client = server.CreateClient();
                var requestUri = new Uri($"{JobsUrl}/{job.Id}", UriKind.Relative);

                var resp = await client.DeleteAsync(requestUri);
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

                using (var scope = server.Host.Services.CreateScope())
                {
                    var anotherContext = scope.ServiceProvider.GetService<WorkshopDbContext>();
                    Assert.IsNull(anotherContext.Jobs.Find(job.Id));
                }
            }
        }
    }
}