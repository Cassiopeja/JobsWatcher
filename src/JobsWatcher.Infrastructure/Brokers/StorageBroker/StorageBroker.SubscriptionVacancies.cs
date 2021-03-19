using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using JobsWatcher.Infrastructure.Extensions;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<SubscriptionVacancy> SubscriptionVacancies { get; set; }
        public IQueryable<SubscriptionVacancy> SelectAllSubscriptionVacancies()
        {
            return SubscriptionVacancies.AsQueryable();
        }

        public async Task<SubscriptionVacancy> SelectSubscriptionVacancyByIdAsync(int subscriptionVacancyId)
        {
            return await SubscriptionVacancies
                .IncludeAllProperties()
                .FirstOrDefaultAsync(subscriptionVacancy => subscriptionVacancy.Id == subscriptionVacancyId);
        }

        public async Task<SubscriptionVacancy> SelectSubscriptionVacancyByVacancyAndSubscriptionIdAsync(int subscriptionId, int vacancyId)
        {
            return await SubscriptionVacancies
                .IncludeAllProperties()
                .FirstOrDefaultAsync(subscriptionVacancy => subscriptionVacancy.SourceSubscriptionId == subscriptionId && 
                                                            subscriptionVacancy.VacancyId == vacancyId);
        }

        public async Task<SubscriptionVacancy> SelectAllSubscriptionVacanciesBySubscriptionIdAsync(int subscriptionId)
        {
            return await SubscriptionVacancies
                .IncludeAllProperties()
                .FirstOrDefaultAsync(subscriptionVacancy => subscriptionVacancy.SourceSubscriptionId == subscriptionId);
        }

        public async Task<SubscriptionVacancy> InsertSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy)
        {
            var subscriptionVacancyEntry = await SubscriptionVacancies.AddAsync(subscriptionVacancy);
            await SaveChangesAsync();
            return subscriptionVacancyEntry.Entity;
        }

        public async Task<SubscriptionVacancy> UpdateSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy)
        {
            var local = SubscriptionVacancies
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(subscriptionVacancy.Id));

            if (local != null)
                Entry(local).State = EntityState.Detached;
            Entry(subscriptionVacancy).State = EntityState.Modified;
            var subscriptionVacancyEntry = SubscriptionVacancies.Update(subscriptionVacancy);
            await SaveChangesAsync();
            return subscriptionVacancyEntry.Entity;
        }

        public async Task<SubscriptionVacancy> DeleteSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy)
        {
            var subscriptionVacancyEntry = SubscriptionVacancies.Remove(subscriptionVacancy);
            await SaveChangesAsync();
            return subscriptionVacancyEntry.Entity;
        }
    }
}