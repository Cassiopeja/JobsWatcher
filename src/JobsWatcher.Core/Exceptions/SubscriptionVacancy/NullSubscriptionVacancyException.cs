using System;

namespace JobsWatcher.Core.Exceptions.SubscriptionVacancy
{
    public class NullSubscriptionVacancyException : Exception
    {
        public NullSubscriptionVacancyException() : base("The subscription vacancy is null.")
        {
        }
    }
}