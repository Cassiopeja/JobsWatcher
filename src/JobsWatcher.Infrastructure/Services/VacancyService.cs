using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using JobsWatcher.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class VacancyService : IVacancyService
    {
        private readonly ILogger<VacancyService> _logger;
        private readonly IStorageBroker _storageBroker;

        public VacancyService(IStorageBroker storageBroker, ILogger<VacancyService> logger)
        {
            _storageBroker = storageBroker;
            _logger = logger;
        }

        public async Task<PagedItems<Vacancy>> GetVacanciesAsync(PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null)
                return await _storageBroker.SelectAllVacancies()
                    .IncludeAllProperties()
                    .ToPagedItemsAsync();

            return await _storageBroker.SelectAllVacancies()
                .IncludeAllProperties()
                .ToPagedItemsAsync(paginationFilter.PageNumber, paginationFilter.PageSize);
        }

        public Task<Vacancy> GetVacancyByIdAsync(int vacancyId)
        {
            return TryCatch(async () =>
            {
                var vacancy = await _storageBroker.SelectVacancyByIdAsync(vacancyId);
                ValidateStorageVacancy(vacancy, vacancyId);
                return vacancy;
            });
        }
    }
}