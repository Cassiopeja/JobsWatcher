using System;

namespace JobsWatcher.Core.Exceptions.SourceReference
{
    public class SourceReferenceValidationException : Exception
    {
        public SourceReferenceValidationException(Exception innerException)
            : base("Invalid input, contact support.", innerException)
        {
        }
    }
}