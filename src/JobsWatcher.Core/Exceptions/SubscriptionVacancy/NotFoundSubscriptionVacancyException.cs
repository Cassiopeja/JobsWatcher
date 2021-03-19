using System;

namespace JobsWatcher.Core.Exceptions.SubscriptionVacancy
{
    public class NotFoundSubscriptionVacancyException : Exception
    {
        public NotFoundSubscriptionVacancyException(int subscriptionVacancyId)
            : base($"Couldn't find subscription vacancy with Id: {subscriptionVacancyId}.")
        {
        } 
    }
}