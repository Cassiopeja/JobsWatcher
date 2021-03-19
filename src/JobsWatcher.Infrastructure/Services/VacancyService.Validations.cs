using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Exceptions.Vacancy;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class VacancyService
    {
        private static void ValidateStorageVacancy(Vacancy storageVacancy, int vacancyId)
        {
            if (storageVacancy == null) throw new NotFoundVacancyException(vacancyId);
        }
    }
}