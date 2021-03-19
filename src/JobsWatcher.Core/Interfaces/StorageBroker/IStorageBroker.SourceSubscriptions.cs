using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<SourceSubscription> SelectAllSourceSubscriptions();
        Task<SourceSubscription> SelectSourceSubscriptionByIdAsync(int sourceSubscriptionId);
        Task<SourceSubscription> InsertSourceSubscriptionAsync(SourceSubscription sourceSubscription);
        Task<SourceSubscription> UpdateSourceSubscriptionAsync(SourceSubscription sourceSubscription);
        Task<SourceSubscription> DeleteSourceSubscriptionAsync(SourceSubscription sourceSubscription);
    }
}