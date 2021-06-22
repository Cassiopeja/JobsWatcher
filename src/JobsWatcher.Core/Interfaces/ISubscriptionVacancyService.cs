using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces
{
    public interface ISubscriptionVacancyService
    {
        Task<PagedItems<SubscriptionVacancy>> GetSubscriptionVacanciesAsync(int subscriptionId,
            GetAllSubscriptionVacanciesFilter filter = null,
            PaginationFilter paginationFilter = null, SortByOptions sortByOptions = null);

        Task<PagedItems<SubscriptionVacancy>> GetSubscriptionVacanciesGroupByCompanyAsync(int subscriptionId,
            GetAllSubscriptionVacanciesFilter filter = null,
            PaginationFilter paginationFilter = null);

        Task<SubscriptionVacancy> GetSubscriptionVacancyByIdAsync(int subscriptionVacancyId);
        Task<SubscriptionVacancy> AddSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy);
        Task<SubscriptionVacancy> UpdateSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy);
        Task<SubscriptionVacancy> DeleteSubscriptionVacancyByIdAsync(int subscriptionVacancyId);
        Task<List<SubscriptionVacancy>> GetSimilarSubscriptionVacancies(int subscriptionVacancyId);
    }
}