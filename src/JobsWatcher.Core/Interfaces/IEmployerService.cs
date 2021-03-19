using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces
{
    public interface IEmployerService
    {
        Task<List<Employer>> GetEmployersAsync();
        Task<List<Employer>> GetEmployersBySubscriptionIdAsync(int subscriptionId);
    }
}