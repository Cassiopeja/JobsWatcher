using System.Threading.Tasks;
using JobsWatcher.Core.Interfaces;

namespace JobsWatcher.Api.IntegrationTests.Stubs
{
    public class StubSubscriptionService : ISyncSubscriptionService
    {
        public Task UpdateSubscription(int subscriptionId)
        { 
            return Task.CompletedTask;
        }

        public Task ArchiveSubscription()
        {
            return Task.CompletedTask;
        }
    }
}