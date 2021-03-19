using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<Area> SelectAllAreas();
        Task<Area> SelectAreaByIdAsync(int areaId);
        Task<Area> InsertAreaAsync(Area area);
        Task<Area> UpdateAreaAsync(Area area);
        Task<Area> DeleteAreaAsync(Area area);
    }
}