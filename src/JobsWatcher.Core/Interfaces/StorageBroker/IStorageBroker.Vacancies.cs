using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<Vacancy> SelectAllVacancies();
        Task<Vacancy> SelectVacancyByIdAsync(int vacancyId);
        Task<Vacancy> SelectVacancyBySourceIdAsync(int sourceTypeId, string sourceId);
        Task<Vacancy> InsertVacancyAsync(Vacancy vacancy);
        Task<Vacancy> UpdateVacancyAsync(Vacancy vacancy);
        Task<Vacancy> DeleteVacancyAsync(Vacancy vacancy);
    }
}