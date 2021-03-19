using System;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using JobsWatcher.Infrastructure.Brokers.HeadHunter;
using JobsWatcher.Infrastructure.Brokers.StorageBroker;
using JobsWatcher.Infrastructure.Jobs;
using JobsWatcher.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Refit;

namespace JobsWatcher.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IStorageBroker, StorageBroker>();
            services.AddDbContext<StorageBroker>(options =>
                    options
                        .UseNpgsql(connectionString, b => b.MigrationsAssembly("JobsWatcher.Infrastructure"))
                        .UseLowerCaseNamingConvention()
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                // .EnableSensitiveDataLogging()
            );
        }

        public static void AddHeadHunterApi(this IServiceCollection services)
        {
            services.AddTransient<HeadHunterHeaderHandler>();
            services.AddRefitClient<IHeadHunterApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.hh.ru/"))
                .AddHttpMessageHandler<HeadHunterHeaderHandler>();
            services.AddScoped<IHeadHunterBroker, HeadHunterBroker>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISourceReferenceService, SourceReferenceService>();
            services.AddScoped<ISubscriptionVacancyService, SubscriptionVacancyService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IEmploymentService, EmploymentService>();
            services.AddScoped<IEmployerService, EmployerService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ISyncSubscriptionService, SyncSubscriptionService>();
        }

        public static void AddSourceHostedServices(this IServiceCollection services)
        {
            services.AddHeadHunterApi();
            services.AddScoped<ISourceService, HeadHunterService>();
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                q.UseSimpleTypeLoader();

                var allSubscriptionsJobKey = new JobKey(nameof(SyncAllSubscriptionsVacanciesJob));
                q.AddJob<SyncAllSubscriptionsVacanciesJob>(opt => opt.WithIdentity(allSubscriptionsJobKey).StoreDurably());
                var singleSubscriptionJobKey = new JobKey(nameof(SyncSingleSubscriptionVacanciesJob));
                q.AddJob<SyncSingleSubscriptionVacanciesJob>(opt => opt.WithIdentity(singleSubscriptionJobKey).StoreDurably());
                // q.AddTrigger(opts => opts
                //     .ForJob(allSubscriptionsJobKey)
                //     .WithIdentity(allSubscriptionsJobKey.Name + " trigger")
                //     .StartNow()
                //     .WithSimpleSchedule(x => x
                //         .WithInterval(TimeSpan.FromDays(1))
                //         .RepeatForever()
                //     )
                // );
                var archiveVacanciesJobKey = new JobKey(nameof(ArchiveVacanciesJob));
                q.AddJob<ArchiveVacanciesJob>(opt => opt.WithIdentity(archiveVacanciesJobKey).StoreDurably());
                // q.AddTrigger(opts => opts
                //     .ForJob(archiveVacanciesJobKey)
                //     .WithIdentity(archiveVacanciesJobKey.Name + " trigger")
                //     .StartNow()
                //     .WithSimpleSchedule(x => x
                //         .WithInterval(TimeSpan.FromDays(1))
                //         .RepeatForever()
                //     )
                // );
            });
            services.AddQuartzServer(q => q.WaitForJobsToComplete = true);
            // services.AddHostedService<UpdateHashService>();
            // services.AddHostedService<SyncVacanciesService>();
            // services.AddHostedService<ArchiveVacanciesService>();
        }
    }
}