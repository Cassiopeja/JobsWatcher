using System;
using System.Threading.Tasks;
using JobsWatcher.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JobsWatcher.Infrastructure.Jobs
{
    public class ArchiveVacanciesJob: IJob
    {
        private readonly ISourceService _sourceService;
        private readonly ILogger<ArchiveVacanciesJob> _logger;

        public ArchiveVacanciesJob(ISourceService sourceService, ILogger<ArchiveVacanciesJob> logger)
        {
            _sourceService = sourceService;
            _logger = logger;
        }
        
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Archiving vacancies");
            try
            {
                await _sourceService.UpdateArchivedAsync();
                _logger.LogInformation("All closed vacancies has been archived");
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while archiving vacancies {ExceptionMessage}", exception.Message);
            }
        }
    }
}