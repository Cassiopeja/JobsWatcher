using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<SourceEmployer> SelectAllSourceEmployers();
        Task<SourceEmployer> SelectSourceEmployerByIdAsync(int sourceEmployerId);
        Task<SourceEmployer> SelectSourceEmployerByIdAndTypeIdAsync(int sourceTypeId, string sourceEmployerId);
        Task<SourceEmployer> InsertSourceEmployerAsync(SourceEmployer sourceEmployer);
        Task<SourceEmployer> UpdateSourceEmployerAsync(SourceEmployer sourceEmployer);
        Task<SourceEmployer> DeleteSourceEmployerAsync(SourceEmployer sourceEmployer);
    }
}