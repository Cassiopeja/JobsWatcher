using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Core.Interfaces
{
    public interface ISourceReferenceService
    {
        Task<SourceReference> GetSourceReferenceBySourceTypeIdAsync(int sourceTypeId);
        Task<IEnumerable<SourceReference>> GetAllSourceReferencesAsync();
    }
}