using System.Threading.Tasks;

namespace JobsWatcher.Core.Interfaces
{
    public interface ISyncSubscriptionService
    {
        Task UpdateSubscription(int subscriptionId);
        Task ArchiveSubscription();
    }
}