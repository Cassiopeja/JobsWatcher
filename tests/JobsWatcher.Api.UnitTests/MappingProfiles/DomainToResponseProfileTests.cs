using AutoMapper;
using JobsWatcher.Api.MappingProfile;
using Xunit;

namespace JobsWatcher.Api.UnitTests.MappingProfiles
{
    public class DomainToResponseProfileTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToResponseProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}