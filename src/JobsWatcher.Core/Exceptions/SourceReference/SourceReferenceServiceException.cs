using System;

namespace JobsWatcher.Core.Exceptions.SourceReference
{
    public class SourceReferenceServiceException : Exception
    {
        public SourceReferenceServiceException(Exception innerException) : base(
            "Service error occurred, contact support.",
            innerException)
        {
        }
    }
}