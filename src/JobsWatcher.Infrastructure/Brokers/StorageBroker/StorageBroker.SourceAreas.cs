using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<SourceArea> SourceAreas { get; set; }

        public IQueryable<SourceArea> SelectAllSourceAreas()
        {
            return SourceAreas.AsQueryable();
        }

        public async Task<SourceArea> SelectSourceAreaByIdAsync(int sourceAreaId)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await SourceAreas.Include(x => x.Area).FirstOrDefaultAsync(x => x.Id == sourceAreaId);
        }

        public async Task<SourceArea> SelectSourceAreaByIdAndTypeIdAsync(int sourceTypeId, string sourceAreaId)
        {
            return await SourceAreas.Include(x => x.Area).FirstOrDefaultAsync(area =>
                area.SourceTypeId == sourceTypeId && area.SourceId == sourceAreaId);
        }

        public async Task<SourceArea> InsertSourceAreaAsync(SourceArea sourceArea)
        {
            var sourceAreaEntry = await SourceAreas.AddAsync(sourceArea);
            await SaveChangesAsync();
            return sourceAreaEntry.Entity;
        }

        public async Task<SourceArea> UpdateSourceAreaAsync(SourceArea sourceArea)
        {
            var sourceAreaEntry = SourceAreas.Update(sourceArea);
            await SaveChangesAsync();
            return sourceAreaEntry.Entity;
        }

        public async Task<SourceArea> DeleteSourceAreaAsync(SourceArea sourceArea)
        {
            var sourceAreaEntry = SourceAreas.Remove(sourceArea);
            await SaveChangesAsync();
            return sourceAreaEntry.Entity;
        }
    }
}