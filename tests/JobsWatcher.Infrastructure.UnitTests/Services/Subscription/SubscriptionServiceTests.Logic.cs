using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using JobsWatcher.Infrastructure.UnitTests.Extensions;
using Moq;
using Xunit;

namespace JobsWatcher.Infrastructure.UnitTests.Services.Subscription
{
    public partial class SubscriptionServiceTests
    {
        [Fact]
        public async Task ShouldAddSubscriptionAsync()
        {
            // Arrange
            var randomSubscription = GetRandomSubscription();
            var inputSubscription = randomSubscription;
            var storageSubscription = randomSubscription;
            var expectedSubscription = randomSubscription.DeepClone();

            _storageBrokerMock.Setup(broker => broker.InsertSourceSubscriptionAsync(inputSubscription))
                .ReturnsAsync(storageSubscription);
            // Act
            var actualSubscription = await _subscriptionService.AddSubscriptionAsync(inputSubscription);

            // Assert
            actualSubscription.Should().BeEquivalentTo(expectedSubscription);
            _storageBrokerMock.Verify(broker => broker.InsertSourceSubscriptionAsync(inputSubscription), Times.Once);
            _storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldUpdateSubscriptionAsync()
        {
            // Arrange
            var randomSubscription = GetRandomSubscription();
            var beforeUpdateSubscription = randomSubscription;
            var inputSubscription = randomSubscription.DeepClone();
            inputSubscription.Name = GetRandomString();
            var afterUpdateSubscription = inputSubscription;
            var expectedSubscription = afterUpdateSubscription.DeepClone();
            var subscriptionId = inputSubscription.Id;

            _storageBrokerMock.Setup(broker => broker.SelectSourceSubscriptionByIdAsync(subscriptionId))
                .ReturnsAsync(beforeUpdateSubscription);
            _storageBrokerMock.Setup(broker => broker.UpdateSourceSubscriptionAsync(inputSubscription))
                .ReturnsAsync(afterUpdateSubscription);
            // Act
            var actualSubscription = await _subscriptionService.UpdateSubscriptionAsync(inputSubscription);

            // Assert
            actualSubscription.Should().BeEquivalentTo(expectedSubscription);
            _storageBrokerMock.Verify(broker => broker.SelectSourceSubscriptionByIdAsync(subscriptionId), Times.Once);
            _storageBrokerMock.Verify(broker => broker.UpdateSourceSubscriptionAsync(inputSubscription), Times.Once);
            _storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDeleteSubscriptionAsync()
        {
            // Arrange
            var randomSubscription = GetRandomSubscription();
            var subscriptionId = randomSubscription.Id;
            var storageSubscription = randomSubscription;
            var expectedSubscription = storageSubscription;

            _storageBrokerMock.Setup(broker => broker.SelectSourceSubscriptionByIdAsync(subscriptionId))
                .ReturnsAsync(storageSubscription);
            _storageBrokerMock.Setup(broker => broker.DeleteSourceSubscriptionAsync(storageSubscription))
                .ReturnsAsync(expectedSubscription);
            // Act
            var actualSubscription = await _subscriptionService.DeleteSubscriptionByIdAsync(subscriptionId);

            // Assert
            actualSubscription.Should().BeEquivalentTo(expectedSubscription);
            _storageBrokerMock.Verify(broker => broker.SelectSourceSubscriptionByIdAsync(subscriptionId), Times.Once);
            _storageBrokerMock.Verify(broker => broker.DeleteSourceSubscriptionAsync(storageSubscription), Times.Once);
            _storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldGetAllSubscriptionAsync()
        {
            // Arrange
            var randomSubscriptions = GetRandomSubscriptions(5);
            var storageSubscriptions = randomSubscriptions.AsAsyncQueryable();
            var expectedSubscriptions = storageSubscriptions.ToList();

            _storageBrokerMock.Setup(broker => broker.SelectAllSourceSubscriptions())
                .Returns(storageSubscriptions);
            // Act
            var actualSubscriptions = await _subscriptionService.GetSubscriptionsAsync();

            // Assert
            actualSubscriptions.Should().BeEquivalentTo(expectedSubscriptions);
            _storageBrokerMock.Verify(broker => broker.SelectAllSourceSubscriptions(), Times.Once);
            _storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldGetSubscriptionByIdAsync()
        {
            // Arrange
            var randomSubscription = GetRandomSubscription();
            var subscriptionId = randomSubscription.Id;
            var storageSubscription = randomSubscription;
            var expectedSubscription = storageSubscription;

            _storageBrokerMock.Setup(broker => broker.SelectSourceSubscriptionByIdAsync(subscriptionId))
                .ReturnsAsync(storageSubscription);
            // Act
            var actualSubscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);

            // Assert
            actualSubscription.Should().BeEquivalentTo(expectedSubscription);
            _storageBrokerMock.Verify(broker => broker.SelectSourceSubscriptionByIdAsync(subscriptionId), Times.Once);
            _storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}