using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        private DbSet<SourceReference> SourceReferences { get; set; }

        public IQueryable<SourceReference> SelectAllSourceReferences()
        {
            return SourceReferences.AsQueryable();
        }

        public async Task<SourceReference> SelectSourceReferenceByIdAsync(int sourceReferenceId)
        {
            return await SourceReferences.IncludeAllProperties().FirstOrDefaultAsync(x => x.Id == sourceReferenceId);
        }

        public async Task<SourceReference> SelectSourceReferenceBySourceTypeIdAsync(int sourceTypeId)
        {
            return await SourceReferences.IncludeAllProperties()
                .FirstOrDefaultAsync(x => x.SourceTypeId == sourceTypeId);
        }

        public async Task<SourceReference> InsertSourceReferenceAsync(SourceReference sourceReference)
        {
            var sourceReferenceEntry = await SourceReferences.AddAsync(sourceReference);
            await SaveChangesAsync();
            return sourceReferenceEntry.Entity;
        }

        public async Task<SourceReference> UpdateSourceReferenceAsync(SourceReference sourceReference)
        {
            var local = SourceReferences
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(sourceReference.Id));

            // check if local is not null 
            if (local != null)
                // detach
                Entry(local).State = EntityState.Detached;
            Entry(sourceReference).State = EntityState.Modified;
            var sourceReferenceEntry = SourceReferences.Update(sourceReference);
            await SaveChangesAsync();
            return sourceReferenceEntry.Entity;
        }

        public async Task<SourceReference> DeleteSourceReferenceAsync(SourceReference sourceReference)
        {
            var sourceReferenceEntry = SourceReferences.Remove(sourceReference);
            await SaveChangesAsync();
            return sourceReferenceEntry.Entity;
        }
    }
}