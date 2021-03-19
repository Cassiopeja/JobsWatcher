using System;
using System.Threading.Tasks;
using JobsWatcher.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JobsWatcher.Infrastructure.Jobs
{
    public class SyncAllSubscriptionsVacanciesJob : IJob
    {
        private readonly ISourceService _sourceService;
        private readonly ILogger<SyncAllSubscriptionsVacanciesJob> _logger;

        public SyncAllSubscriptionsVacanciesJob(ISourceService sourceService, ILogger<SyncAllSubscriptionsVacanciesJob> logger)
        {
            _sourceService = sourceService;
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Updating all subscription vacancies");
            try
            {
                await _sourceService.UpdateAllSubscriptions();
                _logger.LogInformation("All subscription was updated");
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while updating subscriptions {ExceptionMessage}", exception.Message);
            }
        }
    }
}