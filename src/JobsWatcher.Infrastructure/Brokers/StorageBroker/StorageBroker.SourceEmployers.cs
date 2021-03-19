using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<SourceEmployer> SourceEmployers { get; set; }

        public IQueryable<SourceEmployer> SelectAllSourceEmployers()
        {
            return SourceEmployers.AsQueryable();
        }

        public async Task<SourceEmployer> SelectSourceEmployerByIdAsync(int sourceEmployerId)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await SourceEmployers.Include(x => x.Employer)
                .FirstOrDefaultAsync(x => x.Id == sourceEmployerId);
        }

        public async Task<SourceEmployer> SelectSourceEmployerByIdAndTypeIdAsync(int sourceTypeId,
            string sourceEmployerId)
        {
            return await SourceEmployers.Include(x => x.Employer)
                .FirstOrDefaultAsync(employer =>
                    employer.SourceTypeId == sourceTypeId && employer.SourceId == sourceEmployerId);
        }

        public async Task<SourceEmployer> InsertSourceEmployerAsync(SourceEmployer sourceEmployer)
        {
            var sourceEmployerEntry = await SourceEmployers.AddAsync(sourceEmployer);
            await SaveChangesAsync();
            await sourceEmployerEntry.Reference(s => s.Employer).LoadAsync();
            return sourceEmployerEntry.Entity;
        }

        public async Task<SourceEmployer> UpdateSourceEmployerAsync(SourceEmployer sourceEmployer)
        {
            var sourceEmployerEntry = SourceEmployers.Update(sourceEmployer);
            await SaveChangesAsync();
            return sourceEmployerEntry.Entity;
        }

        public async Task<SourceEmployer> DeleteSourceEmployerAsync(SourceEmployer sourceEmployer)
        {
            var sourceEmployerEntry = SourceEmployers.Remove(sourceEmployer);
            await SaveChangesAsync();
            return sourceEmployerEntry.Entity;
        }
    }
}