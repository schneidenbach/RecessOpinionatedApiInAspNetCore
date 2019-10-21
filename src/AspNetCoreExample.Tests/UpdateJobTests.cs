using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Api;
using AspNetCoreWorkshop.Api.Jobs.GetJob;
using AspNetCoreWorkshop.Api.Jobs.GetJobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AspNetCoreWorkshop.Tests
{
    public class UpdateJobTests : TestBase
    {
        private const string JobsUrl = "/api/jobs";

        [Test]
        public async Task Update_job_response_returns_not_found_when_job_does_not_exist()
        {
            using (var server = CreateTestServer())
            {
                var client = server.CreateClient();
                var resp = await client.GetAsync(JobsUrl + "/100000");
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public async Task Update_job_response_returns_data_correctly_updated()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<WorkshopDbContext>();
                var job = context.Jobs.First();
                
                var client = server.CreateClient();
                var requestUri = new Uri($"{JobsUrl}/{job.Id}", UriKind.Relative);

                var newName = "updatin' the name";
                var newDescription = "updatin' the description";
                
                var resp = await client.PutAsJsonAsync(requestUri, new
                {
                    Name = newName,
                    Description = newDescription
                });
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var json = await resp.Content.ReadAsAsync<GetJobResponse>();
                Assert.That(json.Name, Is.EqualTo(newName));
                
                using (var scope = server.Host.Services.CreateScope())
                {
                    var anotherContext = scope.ServiceProvider.GetService<WorkshopDbContext>();
                    var updatedJob = await anotherContext.Jobs.FindAsync(job.Id);
                    
                    Assert.That(updatedJob.Name, Is.EqualTo(newName));
                    Assert.That(updatedJob.Description, Is.EqualTo(newDescription));
                }
            }
        }

        [Test]
        public async Task Update_job_response_returns_bad_request_when_trying_to_use_wrong_data_type()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<WorkshopDbContext>();
                var job = context.Jobs.First();
                
                var client = server.CreateClient();
                var requestUri = new Uri($"{JobsUrl}/{job.Id}", UriKind.Relative);

                var invalidNumber = "an invalid number";
                
                var resp = await client.PutAsJsonAsync(requestUri, new
                {
                    NumberOfProjectManagers = invalidNumber
                });
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

                var contentJson = await resp.Content.ReadAsAsync<ValidationProblemDetails>();
                Assert.That(contentJson, Is.Not.Null);
                Assert.That(contentJson.Errors.ContainsKey("NumberOfProjectManagers"), Is.True);
                Assert.That(contentJson.Errors.Any(e => e.Value.Contains("Property 'NumberOfProjectManagers' has an invalid type specified.")));
            }
        }
    }
}