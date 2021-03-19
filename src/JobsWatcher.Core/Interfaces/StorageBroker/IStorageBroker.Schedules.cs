using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<Schedule> SelectAllSchedules();
        Task<Schedule> SelectScheduleByIdAsync(int scheduleId);
        Task<Schedule> InsertScheduleAsync(Schedule schedule);
        Task<Schedule> UpdateScheduleAsync(Schedule schedule);
        Task<Schedule> DeleteScheduleAsync(Schedule schedule);
    }
}