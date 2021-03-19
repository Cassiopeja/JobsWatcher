using System;

namespace JobsWatcher.Core.Exceptions.Subscription
{
    public class NotFoundSubscriptionException : Exception
    {
        public NotFoundSubscriptionException(int subscriptionId)
            : base($"Couldn't find subscription with Id: {subscriptionId}.")
        {
        }
    }
}