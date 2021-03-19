using System;

namespace JobsWatcher.Core.Exceptions.Subscription
{
    public class NullSubscriptionException : Exception
    {
        public NullSubscriptionException() : base("The subscription is null.")
        {
        }
    }
}