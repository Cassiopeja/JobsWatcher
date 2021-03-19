using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using JobsWatcher.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SubscriptionService : ISubscriptionService
    {
        private readonly ILogger<SubscriptionService> _logger;
        private readonly ISyncSubscriptionService _syncSubscriptionService;
        private readonly IStorageBroker _storageBroker;

        public SubscriptionService(IStorageBroker storageBroker, ILogger<SubscriptionService> logger,
            ISyncSubscriptionService syncSubscriptionService)
        {
            _storageBroker = storageBroker;
            _logger = logger;
            _syncSubscriptionService = syncSubscriptionService;
        }

        public Task<List<SourceSubscription>> GetSubscriptionsAsync()
        {
            return TryCatch(async () =>
            {
                var subscriptions = _storageBroker.SelectAllSourceSubscriptions().IncludeAllProperties();
                return await subscriptions.ToListAsync();
            });
        }

        public Task<SourceSubscription> GetSubscriptionByIdAsync(int subscriptionId)
        {
            return TryCatch(async () =>
            {
                var storageSubscription = await _storageBroker.SelectSourceSubscriptionByIdAsync(subscriptionId);
                ValidateStorageSubscription(storageSubscription, subscriptionId);
                return storageSubscription;
            });
        }

        public Task<SourceSubscription> AddSubscriptionAsync(SourceSubscription subscription)
        {
            return TryCatch(async () =>
            {
                ValidateSubscriptionOnAdd(subscription);
                subscription.Id = 0;
                var storageSubscription = await _storageBroker.InsertSourceSubscriptionAsync(subscription);
                await UpdateSubscriptionVacanciesAsync(storageSubscription);
                return storageSubscription;
            });
        }

        public async Task UpdateSubscriptionVacanciesAsync(SourceSubscription subscription)
        {
            await _syncSubscriptionService.UpdateSubscription(subscription.Id);
        }
        
        public async Task ArchiveSubscriptionVacanciesAsync()
        {
            await _syncSubscriptionService.ArchiveSubscription();
        }

        public Task<SourceSubscription> UpdateSubscriptionAsync(SourceSubscription subscription)
        {
            return TryCatch(async () =>
            {
                ValidateSubscriptionOnUpdate(subscription);
                var storageSubscription = await _storageBroker.SelectSourceSubscriptionByIdAsync(subscription.Id);
                ValidateStorageSubscription(storageSubscription, subscription.Id);
                var updatedSubscription = await _storageBroker.UpdateSourceSubscriptionAsync(subscription);
                if (updatedSubscription.Parameters != storageSubscription.Parameters)
                {
                    await _syncSubscriptionService.UpdateSubscription(storageSubscription.Id);
                }

                return updatedSubscription;
            });
        }

        public Task<SourceSubscription> DeleteSubscriptionByIdAsync(int subscriptionId)
        {
            return TryCatch(async () =>
            {
                var storageSubscription = await _storageBroker.SelectSourceSubscriptionByIdAsync(subscriptionId);
                ValidateStorageSubscription(storageSubscription, subscriptionId);
                return await _storageBroker.DeleteSourceSubscriptionAsync(storageSubscription);
            });
        }
    }
}