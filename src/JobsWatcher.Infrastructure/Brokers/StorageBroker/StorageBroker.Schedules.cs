using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<Schedule> Schedules { get; set; }

        public IQueryable<Schedule> SelectAllSchedules()
        {
            return Schedules.AsQueryable();
        }

        public async Task<Schedule> SelectScheduleByIdAsync(int scheduleId)
        {
            return await Schedules.FindAsync(scheduleId);
        }

        public async Task<Schedule> InsertScheduleAsync(Schedule schedule)
        {
            var scheduleEntry = await Schedules.AddAsync(schedule);
            await SaveChangesAsync();
            return scheduleEntry.Entity;
        }

        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
        {
            var scheduleEntry = Schedules.Update(schedule);
            await SaveChangesAsync();
            return scheduleEntry.Entity;
        }

        public async Task<Schedule> DeleteScheduleAsync(Schedule schedule)
        {
            var scheduleEntry = Schedules.Remove(schedule);
            await SaveChangesAsync();
            return scheduleEntry.Entity;
        }
    }
}