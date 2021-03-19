using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<Employment> SelectAllEmployments();
        Task<Employment> SelectEmploymentByIdAsync(int employmentId);
        Task<Employment> InsertEmploymentAsync(Employment employment);
        Task<Employment> UpdateEmploymentAsync(Employment employment);
        Task<Employment> DeleteEmploymentAsync(Employment employment);
    }
}