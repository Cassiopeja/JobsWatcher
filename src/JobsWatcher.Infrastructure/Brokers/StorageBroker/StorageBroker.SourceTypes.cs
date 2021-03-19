using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<SourceType> SourceTypes { get; set; }

        public IQueryable<SourceType> SelectAllSourceTypes()
        {
            return SourceTypes.AsQueryable();
        }

        public async Task<SourceType> SelectSourceTypeByIdAsync(int sourceTypeId)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await SourceTypes.FindAsync(sourceTypeId);
        }

        public async Task<SourceType> InsertSourceTypeAsync(SourceType sourceType)
        {
            var sourceTypeEntry = await SourceTypes.AddAsync(sourceType);
            await SaveChangesAsync();
            return sourceTypeEntry.Entity;
        }

        public async Task<SourceType> UpdateSourceTypeAsync(SourceType sourceType)
        {
            var sourceTypeEntry = SourceTypes.Update(sourceType);
            await SaveChangesAsync();
            return sourceTypeEntry.Entity;
        }

        public async Task<SourceType> DeleteSourceTypeAsync(SourceType sourceType)
        {
            var sourceTypeEntry = SourceTypes.Remove(sourceType);
            await SaveChangesAsync();
            return sourceTypeEntry.Entity;
        }
    }
}