using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AspNetCoreWorkshop.Tests
{
    public class MapperTests : TestBase
    {
        [Test]
        public void AssertMapperConfigurationIsValid()
        {
            using (var server = CreateTestServer())
            {
                server.Host.Services.GetService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
            }
        }
    }
}