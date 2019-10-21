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
    public class GetJobTests : TestBase
    {
        private const string JobsUrl = "/api/jobs";

        [Test]
        public async Task Get_jobs_response_returns_not_found_when_job_does_not_exist()
        {
            using (var server = CreateTestServer())
            {
                var client = server.CreateClient();
                var resp = await client.GetAsync(JobsUrl + "/100000");
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public async Task Get_job_response_returns_correct_data()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<WorkshopDbContext>();
                var job = context.Jobs.First();
                
                var client = server.CreateClient();
                var requestUri = new Uri($"{JobsUrl}/{job.Id}", UriKind.Relative);

                var resp = await client.GetAsync(requestUri);
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var json = await resp.Content.ReadAsAsync<GetJobResponse>();
                Assert.That(json.Name, Is.EqualTo(job.Name));
                Assert.That(json.Number, Is.EqualTo(job.Number));
            }
        }
    }
}