using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Exceptions.Subscription;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SubscriptionService
    {
        private void ValidateStorageSubscription(SourceSubscription subscription, int subscriptionId)
        {
            if (subscription == null) throw new NotFoundSubscriptionException(subscriptionId);
        }

        private void ValidateSubscriptionOnUpdate(SourceSubscription subscription)
        {
            ValidateNotNullSubscription(subscription);
        }

        private void ValidateSubscriptionOnAdd(SourceSubscription subscription)
        {
            ValidateNotNullSubscription(subscription);
        }

        private void ValidateNotNullSubscription(SourceSubscription subscription)
        {
            if (subscription == null) throw new NullSubscriptionException();
        }
    }
}