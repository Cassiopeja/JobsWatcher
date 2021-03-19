using System;
using JobsWatcher.Api.IntegrationTests.Helpers;
using JobsWatcher.Api.IntegrationTests.Stubs;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Infrastructure.Brokers.StorageBroker;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace JobsWatcher.Api.IntegrationTests.Controllers
{
    public class CustomWebApplicationTestFixture : WebApplicationFactory<Program>
    {
        public ITestOutputHelper OutputHelper { get; set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = base.CreateHostBuilder();
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders(); // Remove other loggers
                logging.Services.RemoveAll(typeof(ILoggerFactory));
            });

            return builder;
        }

        protected override void ConfigureWebHost(
            IWebHostBuilder builder)
        {
            builder.ConfigureLogging(logging => { logging.AddXUnit(OutputHelper); });
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<ISyncSubscriptionService, StubSubscriptionService>();

                services.RemoveAll(typeof(DbContextOptions<StorageBroker>));
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<StorageBroker>(options =>
                {
                    options
                        .UseInMemoryDatabase("InMemoryDbForTesting")
                        .UseInternalServiceProvider(serviceProvider)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                using var db = scopedServices.GetRequiredService<StorageBroker>();
                try
                {
                    db.Database.EnsureCreated();
                    Utilities.InitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    OutputHelper.WriteLine("An error occurred seeding the " +
                                           "database with test messages. Error: {0}", ex.Message);
                    OutputHelper.WriteLine(ex.ToString());
                }
            });
        }
    }
}