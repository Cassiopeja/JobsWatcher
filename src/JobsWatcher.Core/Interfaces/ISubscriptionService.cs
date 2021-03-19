using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Core.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<SourceSubscription>> GetSubscriptionsAsync();
        Task<SourceSubscription> GetSubscriptionByIdAsync(int subscriptionId);
        Task<SourceSubscription> AddSubscriptionAsync(SourceSubscription subscription);
        Task<SourceSubscription> UpdateSubscriptionAsync(SourceSubscription subscription);
        Task<SourceSubscription> DeleteSubscriptionByIdAsync(int subscriptionId);
        Task UpdateSubscriptionVacanciesAsync(SourceSubscription subscription);
        Task ArchiveSubscriptionVacanciesAsync();
    }
}