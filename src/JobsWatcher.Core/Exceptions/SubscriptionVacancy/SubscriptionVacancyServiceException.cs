using System;

namespace JobsWatcher.Core.Exceptions.SubscriptionVacancy
{
    public class SubscriptionVacancyServiceException : Exception
    {
        public SubscriptionVacancyServiceException(Exception innerException) : base(
            "Service error occurred, contact support.",
            innerException)
        {
        }
    }
}