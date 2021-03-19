using System;
using System.Threading.Tasks;
using JobsWatcher.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JobsWatcher.Infrastructure.Jobs
{
    public class SyncSingleSubscriptionVacanciesJob : IJob
    {
        private readonly ILogger<SyncSingleSubscriptionVacanciesJob> _logger;
        private readonly ISourceService _sourceService;

        public SyncSingleSubscriptionVacanciesJob(ISourceService sourceService,
            ILogger<SyncSingleSubscriptionVacanciesJob> logger)
        {
            _sourceService = sourceService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dataMap = context.JobDetail.JobDataMap;
                var subscriptionId = dataMap.GetIntValue("subscriptionId");
                _logger.LogInformation("Updating all vacancies for subscription with id {subscriptionId}", subscriptionId);
                await _sourceService.UpdateSingleSubscription(subscriptionId);
                _logger.LogInformation("Subscription with id {subscriptionId} was updated", subscriptionId);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while updating subscriptions {ExceptionMessage}", exception.Message);
            }
        }
    }
}