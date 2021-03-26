using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Exceptions.Subscription;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SkillService
    {
        private void ValidateSubscription(SourceSubscription subscription, int subscriptionId)
        {
            if (subscription == null) throw new NotFoundSubscriptionException(subscriptionId);
        }
    }
}