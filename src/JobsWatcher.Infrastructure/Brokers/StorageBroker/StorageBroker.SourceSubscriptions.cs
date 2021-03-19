using System;
using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<SourceSubscription> SourceSubscriptions { get; set; }

        public IQueryable<SourceSubscription> SelectAllSourceSubscriptions()
        {
            return SourceSubscriptions.AsQueryable();
        }

        public async Task<SourceSubscription> SelectSourceSubscriptionByIdAsync(int sourceSubscriptionId)
        {
            return await SourceSubscriptions.IncludeAllProperties()
                .FirstOrDefaultAsync(s => s.Id == sourceSubscriptionId);
        }

        public async Task<SourceSubscription> InsertSourceSubscriptionAsync(SourceSubscription sourceSubscription)
        {
            sourceSubscription.CreatedDate = DateTimeOffset.Now;
            sourceSubscription.UpdateDate = DateTimeOffset.Now;
            var sourceSubscriptionEntry = await SourceSubscriptions.AddAsync(sourceSubscription);
            await SaveChangesAsync();
            await Entry(sourceSubscriptionEntry.Entity).Reference(r => r.SourceType).LoadAsync();
            return sourceSubscriptionEntry.Entity;
        }

        public async Task<SourceSubscription> UpdateSourceSubscriptionAsync(SourceSubscription sourceSubscription)
        {
            sourceSubscription.UpdateDate = DateTimeOffset.Now;
            var sourceSubscriptionEntry = SourceSubscriptions.Update(sourceSubscription);
            await SaveChangesAsync();
            await Entry(sourceSubscriptionEntry.Entity).Reference(r => r.SourceType).LoadAsync();
            return sourceSubscriptionEntry.Entity;
        }

        public async Task<SourceSubscription> DeleteSourceSubscriptionAsync(SourceSubscription sourceSubscription)
        {
            var sourceSubscriptionEntry = SourceSubscriptions.Remove(sourceSubscription);
            await SaveChangesAsync();
            return sourceSubscriptionEntry.Entity;
        }
    }
}