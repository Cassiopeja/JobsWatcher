using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<SubscriptionVacancy> SelectAllSubscriptionVacancies();
        Task<SubscriptionVacancy> SelectSubscriptionVacancyByIdAsync(int subscriptionVacancyId);
        Task<SubscriptionVacancy> SelectSubscriptionVacancyByVacancyAndSubscriptionIdAsync(int subscriptionId, int vacancyId);
        Task<SubscriptionVacancy> SelectAllSubscriptionVacanciesBySubscriptionIdAsync(int subscriptionId);
        Task<SubscriptionVacancy> InsertSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy);
        Task<SubscriptionVacancy> UpdateSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy);
        Task<SubscriptionVacancy> DeleteSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy);
    }
}