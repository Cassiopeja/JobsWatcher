using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public class UpdateHashService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public UpdateHashService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _provider.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<UpdateHashService>>();
                    logger.Log(LogLevel.Information, "Begin updating vacancies");
                    try
                    {
                        var storageBroker = scope.ServiceProvider.GetRequiredService<IStorageBroker>();
                        foreach (var vacancy in storageBroker.SelectAllVacancies().ToList())
                        {
                            vacancy.HashCode = "1";
                            await storageBroker.UpdateVacancyAsync(vacancy);
                        }
                    }
                    catch (Exception exception)
                    {
                        logger.Log(LogLevel.Critical, "Error while updating hash for vacancies {ExceptionMessage}",
                            exception.Message);
                    }
                    
                    logger.Log(LogLevel.Information, "End updating vacancies");
                }

                await Task.Delay(100 * 60 * 20, stoppingToken);
            }
        }
    }
}