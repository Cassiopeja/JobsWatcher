using System;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using JobsWatcher.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace JobsWatcher.Infrastructure.UnitTests.Services.Subscription
{
    public partial class SubscriptionServiceTests
    {
        private readonly Mock<ILogger<SubscriptionService>> _loggerMock;
        private readonly Random _randomNumber = new();
        private readonly Mock<IStorageBroker> _storageBrokerMock;
        private readonly SubscriptionService _subscriptionService;
        private Faker<SourceSubscription> _subscriptionFaker;
        private readonly Mock<ISyncSubscriptionService> _syncSubscriptionService;

        public SubscriptionServiceTests()
        {
            _storageBrokerMock = new Mock<IStorageBroker>();
            _loggerMock = new Mock<ILogger<SubscriptionService>>();
            _syncSubscriptionService = new Mock<ISyncSubscriptionService>();
            _subscriptionService = new SubscriptionService(_storageBrokerMock.Object, _loggerMock.Object,
                _syncSubscriptionService.Object);
            SetupFakers();
        }

        private IQueryable<SourceSubscription> GetRandomSubscriptions(int count)
        {
            return _subscriptionFaker.Generate(count).AsQueryable();
        }

        private void SetupFakers()
        {
            _subscriptionFaker = new Faker<SourceSubscription>()
                .RuleFor(s => s.Name, faker => faker.Lorem.Sentence())
                .RuleFor(s => s.Parameters, faker => faker.Lorem.Paragraph())
                .RuleFor(s => s.Id, faker => faker.IndexFaker);
        }

        private SourceSubscription GetRandomSubscription()
        {
            return _subscriptionFaker.Generate();
        }

        private int GetRandomId()
        {
            return _randomNumber.Next();
        }

        private string GetRandomString()
        {
            var lorem = new Lorem();
            return lorem.Word();
        }
    }
}