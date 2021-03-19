using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<SourceArea> SelectAllSourceAreas();
        Task<SourceArea> SelectSourceAreaByIdAsync(int sourceAreaId);
        Task<SourceArea> SelectSourceAreaByIdAndTypeIdAsync(int sourceTypeId, string sourceAreaId);
        Task<SourceArea> InsertSourceAreaAsync(SourceArea sourceArea);
        Task<SourceArea> UpdateSourceAreaAsync(SourceArea sourceArea);
        Task<SourceArea> DeleteSourceAreaAsync(SourceArea sourceArea);
    }
}