using System;
using System.Threading;
using System.Threading.Tasks;
using JobsWatcher.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JobsWatcher.Infrastructure.Services
{
    public class SyncVacanciesService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public SyncVacanciesService(IServiceProvider provider)
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
                    try
                    {
                        await sourceService.UpdateAllSubscriptions();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                await Task.Delay(10 * 60 * 20, stoppingToken);
            }
        }
    }
}