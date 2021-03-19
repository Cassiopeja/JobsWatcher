using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces
{
    public interface IAreaService
    {
        Task<List<Area>> GetAreasAsync();
        Task<List<Area>> GetAreasBySubscriptionIdAsync(int subscriptionId);
    }
}