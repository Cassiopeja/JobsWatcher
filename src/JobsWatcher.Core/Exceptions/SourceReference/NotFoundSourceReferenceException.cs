using System;

namespace JobsWatcher.Core.Exceptions.SourceReference
{
    public class NotFoundSourceReferenceException : Exception
    {
        public NotFoundSourceReferenceException(int sourceTypeId)
            : base($"Couldn't find source reference with source type Id: {sourceTypeId}.")
        {
        }
    }
}