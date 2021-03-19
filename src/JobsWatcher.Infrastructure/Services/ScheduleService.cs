using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IStorageBroker _storageBroker;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(IStorageBroker storageBroker, ILogger<ScheduleService> logger)
        {
            _storageBroker = storageBroker;
            _logger = logger;
        }
        
        public async Task<List<Schedule>> GetSchedulesAsync()
        {
            return await _storageBroker.SelectAllSchedules().ToListAsync();
        }

        public async Task<List<Schedule>> GetSchedulesBySubscriptionIdAsync(int subscriptionId)
        {
            return await _storageBroker
                .SelectAllSubscriptionVacancies()
                .Where(sv => sv.SourceSubscriptionId == subscriptionId 
                             && sv.Vacancy.ScheduleId != null)
                .Select(sv =>
                    new Schedule
                    {
                        Id = sv.Vacancy.Schedule.Id,
                        Name = sv.Vacancy.Schedule.Name
                    }
                )
                .Distinct()
                .ToListAsync();
        }
    }
}