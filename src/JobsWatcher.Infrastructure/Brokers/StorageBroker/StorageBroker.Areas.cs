using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<Area> Areas { get; set; }

        public IQueryable<Area> SelectAllAreas()
        {
            return Areas.AsQueryable();
        }

        public async Task<Area> SelectAreaByIdAsync(int areaId)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await Areas.FindAsync(areaId);
        }

        public async Task<Area> InsertAreaAsync(Area area)
        {
            var areaEntry = await Areas.AddAsync(area);
            await SaveChangesAsync();
            return areaEntry.Entity;
        }

        public async Task<Area> UpdateAreaAsync(Area area)
        {
            var areaEntry = Areas.Update(area);
            await SaveChangesAsync();
            return areaEntry.Entity;
        }

        public async Task<Area> DeleteAreaAsync(Area area)
        {
            var areaEntry = Areas.Remove(area);
            await SaveChangesAsync();
            return areaEntry.Entity;
        }
    }
}