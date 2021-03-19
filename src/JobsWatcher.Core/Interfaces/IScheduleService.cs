using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetSchedulesAsync();
        Task<List<Schedule>> GetSchedulesBySubscriptionIdAsync(int subscriptionId);
    }
}