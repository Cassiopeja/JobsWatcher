using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces
{
    public interface IEmploymentService
    {
        Task<List<Employment>> GetEmploymentsAsync();
        Task<List<Employment>> GetEmploymentsBySubscriptionIdAsync(int subscriptionId);
    }
}