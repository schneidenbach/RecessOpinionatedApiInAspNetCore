using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using OpinionatedApiExample.Shared;

namespace OpinionatedApiExample.Tests
{
    public abstract class TestBase
    {
        protected TestBase()
        {
            Server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = Server.CreateClient();
            Client.Timeout = TimeSpan.FromMinutes(20);            
        }

        public HttpClient Client { get; }

        public TestServer Server { get; }
        
        protected async Task<int> CreateNewSpencyAsync()
        {
            var resp = await Client.PostAsJsonAsync("api/Employees", new
            {
                firstName = "Spencer",
                lastName = "Schneidenbach"
            });
            resp.EnsureSuccessStatusCode();

            var id = (await resp.Content.ReadAsAsync<dynamic>()).id;
            return id;
        }
    }
}