using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OpinionatedApiExample.Tests
{
    public class JobTests : TestBase
    {
        [Test]
        public async Task JobCreatedSuccessfully()
        {
            var id = await CreateNewSpencyAsync();
            
            var resp = await Client.PostAsJsonAsync("api/Jobs", new
            {
                description = "A new build",
                number = "1234",
                projectManagerId = id
            });
            resp.EnsureSuccessStatusCode();
            var jobId = (await resp.Content.ReadAsAsync<dynamic>()).id;

            resp = await Client.GetAsync("api/Jobs/" + jobId);
            resp.EnsureSuccessStatusCode();
            
            var createdJob = await resp.Content.ReadAsAsync<dynamic>();
            
            Assert.That(createdJob.description?.ToString(), Is.EqualTo("A new build"));
            Assert.That(createdJob.number?.ToString(), Is.EqualTo("1234"));
            Assert.That((int) createdJob.projectManager.id, Is.EqualTo(id));
        }
    }
}