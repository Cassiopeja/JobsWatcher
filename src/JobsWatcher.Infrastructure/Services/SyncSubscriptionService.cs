using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JobsWatcher.Infrastructure.Services
{
    public class SyncSubscriptionService : ISyncSubscriptionService
    {
        private readonly ILogger<SyncSubscriptionService> _logger;
        private readonly ISchedulerFactory _schedulerFactory;

        public SyncSubscriptionService(ISchedulerFactory schedulerFactory, ILogger<SyncSubscriptionService> logger)
        {
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task UpdateSubscription(int subscriptionId)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var key = new JobKey("SyncSingleSubscriptionVacanciesJob");
            var job = await scheduler.GetJobDetail(key);
            if (job == null)
            {
                _logger.LogError("Job with key {Key} is not found", key);
                return;
            }

            job.JobDataMap.Put("subscriptionId", subscriptionId);
            await scheduler.AddJob(job, true);
            await scheduler.PauseJob(job.Key);
            var data = new Dictionary<string, int> {["subscriptionId"] = subscriptionId};
            var jobData = new JobDataMap(data);
            await scheduler.TriggerJob(key, jobData);
        }

        public async Task ArchiveSubscription()
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var key = new JobKey("ArchiveVacanciesJob");
            var job = await scheduler.GetJobDetail(key);
            if (job == null)
            {
                _logger.LogError("Job with key {Key} is not found", key);
                return;
            }

            await scheduler.AddJob(job, true);
            await scheduler.PauseJob(job.Key);
            await scheduler.TriggerJob(key);
            
        }
    }
}