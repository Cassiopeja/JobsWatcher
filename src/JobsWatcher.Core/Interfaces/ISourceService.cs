using System.Threading.Tasks;

namespace JobsWatcher.Core.Interfaces
{
    public interface ISourceService
    {
        Task UpdateAllSubscriptions();

        Task UpdateArchivedAsync();
        Task UpdateSingleSubscription(int subscriptionId);
    }
}