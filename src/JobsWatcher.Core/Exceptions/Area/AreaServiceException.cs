using System;

namespace JobsWatcher.Core.Exceptions.Area
{
    public class AreaServiceException : Exception
    {
        public AreaServiceException(Exception innerException) : base("Service error occurred, contact support.",
            innerException)
        {
        }
    }
}