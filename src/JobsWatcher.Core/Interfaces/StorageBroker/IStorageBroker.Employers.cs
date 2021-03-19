using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<Employer> SelectAllEmployers();
        Task<Employer> SelectEmployerByIdAsync(int employerId);
        Task<Employer> InsertEmployerAsync(Employer employer);
        Task<Employer> UpdateEmployerAsync(Employer employer);
        Task<Employer> DeleteEmployerAsync(Employer employer);
    }
}