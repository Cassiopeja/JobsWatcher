using System;

namespace JobsWatcher.Core.Exceptions.Vacancy
{
    public class VacancyServiceException : Exception
    {
        public VacancyServiceException(Exception innerException)
            : base("Service error occurred, contact support.", innerException)
        {
        }
    }
}