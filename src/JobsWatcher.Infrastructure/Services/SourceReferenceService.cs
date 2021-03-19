using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SourceReferenceService : ISourceReferenceService
    {
        private readonly ILogger<SourceReferenceService> _logger;
        private readonly IStorageBroker _storageBroker;

        public SourceReferenceService(IStorageBroker storageBroker, ILogger<SourceReferenceService> logger)
        {
            _storageBroker = storageBroker;
            _logger = logger;
        }

        public Task<SourceReference> GetSourceReferenceBySourceTypeIdAsync(int sourceTypeId)
        {
            return TryCatch(async () =>
            {
                var storageSourceReference =
                    await _storageBroker.SelectSourceReferenceBySourceTypeIdAsync(sourceTypeId);
                ValidateStorageSourceReference(storageSourceReference, sourceTypeId);
                return storageSourceReference;
            });
        }

        public Task<IEnumerable<SourceReference>> GetAllSourceReferencesAsync()
        {
            return TryCatch(async () => await _storageBroker.SelectAllSourceReferences().ToArrayAsync());
        }
    }
}