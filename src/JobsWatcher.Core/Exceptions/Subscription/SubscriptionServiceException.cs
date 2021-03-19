using System;

namespace JobsWatcher.Core.Exceptions.Subscription
{
    public class SubscriptionServiceException : Exception
    {
        public SubscriptionServiceException(Exception innerException) : base("Service error occurred, contact support.",
            innerException)
        {
        }
    }
}