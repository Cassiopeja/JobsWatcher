using System;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter.Exceptions
{
    public class VacancyNotFound: Exception
    {
        public VacancyNotFound(string id):
            base($"Vacancy with id # {id} is not found")
        {
            
        }
    }
}