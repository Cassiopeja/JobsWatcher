using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Exceptions.Subscription;
using JobsWatcher.Core.Exceptions.SubscriptionVacancy;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SubscriptionVacancyService
    {
        private void ValidateStorageSubscriptionVacancy(SubscriptionVacancy storageSubscriptionVacancy, int subscriptionVacancyId)
        {
            if (storageSubscriptionVacancy == null) throw new NotFoundSubscriptionVacancyException(subscriptionVacancyId);
        }

        private void ValidateSubscriptionVacancyOnAdd(SubscriptionVacancy subscriptionVacancy)
        {
            ValidateNotNullSubscriptionVacancy(subscriptionVacancy);
        }
        
        private void ValidateSubscriptionVacancyOnUpdate(SubscriptionVacancy subscriptionVacancy)
        {
            ValidateNotNullSubscriptionVacancy(subscriptionVacancy);
        }
        
        private void ValidateNotNullSubscriptionVacancy(SubscriptionVacancy subscriptionVacancy)
        {
            if (subscriptionVacancy == null) throw new NullSubscriptionVacancyException();
        }

        private void ValidateSubscription(SourceSubscription subscription, int subscriptionId)
        {
            if (subscription == null) throw new NotFoundSubscriptionException(subscriptionId);
        }
    }
}