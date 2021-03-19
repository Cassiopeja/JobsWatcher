using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces
{
    public interface IVacancyService
    {
        Task<PagedItems<Vacancy>> GetVacanciesAsync(PaginationFilter paginationFilter = null);
        Task<Vacancy> GetVacancyByIdAsync(int vacancyId);
    }
}