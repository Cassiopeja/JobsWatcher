using System;

namespace JobsWatcher.Core.Exceptions.Vacancy
{
    public class NotFoundVacancyException : Exception
    {
        public NotFoundVacancyException(int vacancyId)
            : base($"Couldn't find student with Id: {vacancyId}.")
        {
        }
    }
}