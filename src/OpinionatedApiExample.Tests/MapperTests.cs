using NUnit.Framework;
using OpinionatedApiExample.Shared;

namespace OpinionatedApiExample.Tests
{
    public class MapperTests
    {
        [Test]
        public void AssertConfigurationIsValid()
        {
            OpinionatedMapperFactory.CreateMapper().ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}