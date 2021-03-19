using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Exceptions.SourceReference;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SourceReferenceService
    {
        private void ValidateStorageSourceReference(SourceReference storageSourceReference, int sourceTypeId)
        {
            if (storageSourceReference == null) throw new NotFoundSourceReferenceException(sourceTypeId);
        }
    }
}