using System;

namespace JobsWatcher.Core.Exceptions.Subscription
{
    public class SubscriptionValidationException : Exception
    {
        public SubscriptionValidationException(Exception innerException)
            : base("Invalid input, contact support.", innerException)
        {
        }
    }
}