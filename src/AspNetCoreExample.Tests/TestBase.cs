using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Api;
using AspNetCoreWorkshop.Api.Jobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AspNetCoreWorkshop.Tests
{
    public abstract class TestBase
    {
        protected TestServer CreateTestServer()
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<WorkshopDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("test");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<WorkshopDbContext>();

                        db.Database.EnsureCreated();

                        db.Jobs.Add(new Job
                        {
                            Number = "12345-",
                            Name = "Building a Wal-Mart",
                            Description = null,
                            StartDate = new DateTime(2019, 5, 15),
                            JobPhases = new List<JobPhase>
                            {
                                new JobPhase {Description = "test1", Number = "0043"},
                                new JobPhase {Description = "test2", Number = "0044"},
                                new JobPhase {Description = "test3", Number = "0045"},
                            }
                        });

                        db.Jobs.Add(new Job
                        {
                            Number = "George",
                            Name = "George's Day Spa",
                            Description = null,
                            StartDate = new DateTime(2019, 7, 15),
                            JobPhases = new List<JobPhase>
                            {
                                new JobPhase {Description = "test4", Number = "0046"},
                                new JobPhase {Description = "test5", Number = "0047"},
                                new JobPhase {Description = "test6", Number = "0048"},
                            }
                        });

                        db.SaveChanges();
                    }
                })
                .UseStartup<Startup>();

            return new TestServer(hostBuilder);
        }
    }
}