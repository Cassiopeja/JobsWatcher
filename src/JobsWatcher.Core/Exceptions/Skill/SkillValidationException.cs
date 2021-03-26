using System;

namespace JobsWatcher.Core.Exceptions.Skill
{
    public class SkillValidationException : Exception
    {
        public SkillValidationException(Exception innerException) : base("Invalid input, contact support.",
            innerException)
        {
        }
    }
}