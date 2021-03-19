using System;

namespace JobsWatcher.Core.Exceptions.SubscriptionVacancy
{
    public class SubscriptionVacancyValidationException : Exception
    {
        public SubscriptionVacancyValidationException(Exception innerException)
            : base("Invalid input, contact support.", innerException)
        {
        }
    }
}