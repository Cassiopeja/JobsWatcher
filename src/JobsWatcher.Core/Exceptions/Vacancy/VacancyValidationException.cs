using System;

namespace JobsWatcher.Core.Exceptions.Vacancy
{
    public class VacancyValidationException : Exception
    {
        public VacancyValidationException(Exception innerException)
            : base("Invalid input, contact support.", innerException)
        {
        }
    }
}