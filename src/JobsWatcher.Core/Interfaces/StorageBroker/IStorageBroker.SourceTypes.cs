using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<SourceType> SelectAllSourceTypes();
        Task<SourceType> SelectSourceTypeByIdAsync(int sourceTypeId);
        Task<SourceType> InsertSourceTypeAsync(SourceType sourceType);
        Task<SourceType> UpdateSourceTypeAsync(SourceType sourceType);
        Task<SourceType> DeleteSourceTypeAsync(SourceType sourceType);
    }
}