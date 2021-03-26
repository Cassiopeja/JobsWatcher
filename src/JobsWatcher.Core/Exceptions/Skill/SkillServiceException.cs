using System;

namespace JobsWatcher.Core.Exceptions.Skill
{
    public class SkillServiceException : Exception
    {
        public SkillServiceException(Exception innerException) : base("Service error occurred, contact support.",
            innerException)
        {
        }
    }
}