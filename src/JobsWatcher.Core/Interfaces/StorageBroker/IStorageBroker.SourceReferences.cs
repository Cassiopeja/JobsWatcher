using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<SourceReference> SelectAllSourceReferences();
        Task<SourceReference> SelectSourceReferenceByIdAsync(int sourceReferenceId);
        Task<SourceReference> SelectSourceReferenceBySourceTypeIdAsync(int sourceTypeId);
        Task<SourceReference> InsertSourceReferenceAsync(SourceReference sourceReference);
        Task<SourceReference> UpdateSourceReferenceAsync(SourceReference sourceReference);
        Task<SourceReference> DeleteSourceReferenceAsync(SourceReference sourceReference);
    }
}