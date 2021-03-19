using System;
using System.Threading;
using System.Threading.Tasks;
using JobsWatcher.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public class ArchiveVacanciesService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public ArchiveVacanciesService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _provider.CreateScope())
                {
                    var sourceService = scope.ServiceProvider
                        .GetRequiredService<ISourceService>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ArchiveVacanciesService>>();
                    try
                    {
                        await sourceService.UpdateArchivedAsync();
                    }
                    catch (Exception exception)
                    {
                        logger.Log(LogLevel.Critical, "Error while archiving vacancies {ExceptionMessage}", exception.Message);
                    }
                }
                
                await Task.Delay(10 * 60 * 20, stoppingToken);
            }
        }
    }
}