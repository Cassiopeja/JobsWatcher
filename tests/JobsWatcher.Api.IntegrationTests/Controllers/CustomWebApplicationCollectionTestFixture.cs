using Xunit;

namespace JobsWatcher.Api.IntegrationTests.Controllers
{
    [CollectionDefinition("CustomWebApplication")]
    public class CustomWebApplicationCollectionTestFixture:ICollectionFixture<CustomWebApplicationTestFixture>
    {
        
    }
}