using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Exceptions.Subscription;
using Moq;
using Xunit;

namespace JobsWatcher.Infrastructure.UnitTests.Services.Subscription
{
    public partial class SubscriptionServiceTests
    {
        [Fact]
        public async void ShouldThrowValidationExceptionOnGetWhenStorageSubscriptionIsNullAsync()
        {
            // arrange
            var randomSubscriptionId = GetRandomId();
            var inputSubscriptionId = randomSubscriptionId;
            SourceSubscription invalidSubscription = null;
            var notFoundSubscriptionException = new NotFoundSubscriptionException(inputSubscriptionId);

            var expectedValidationException =
                new SubscriptionValidationException(notFoundSubscriptionException);

            _storageBrokerMock.Setup(broker =>
                    broker.SelectSourceSubscriptionByIdAsync(inputSubscriptionId))
                .ReturnsAsync(invalidSubscription);

            // act
            var actualSubscriptionTask = _subscriptionService.GetSubscriptionByIdAsync(inputSubscriptionId);

            // assert
            var actualException = await Assert.ThrowsAsync<SubscriptionValidationException>(() =>
                actualSubscriptionTask);

            Assert.Equal(expectedValidationException.Message, actualException.Message);
            Assert.IsType<NotFoundSubscriptionException>(actualException.InnerException);
            Assert.Equal(expectedValidationException.InnerException.Message, actualException.InnerException.Message);

            _storageBrokerMock.Verify(broker =>
                    broker.SelectSourceSubscriptionByIdAsync(inputSubscriptionId),
                Times.Once);

            _storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowValidationExceptionOnUpdateWhenStorageSubscriptionIsNullAsync()
        {
            // arrange
            var randomSubscription = GetRandomSubscription();
            var inputSubscription = randomSubscription;
            var inputSubscriptionId = randomSubscription.Id;
            SourceSubscription invalidSubscription = null;
            var notFoundSubscriptionException = new NotFoundSubscriptionException(inputSubscriptionId);

            var expectedValidationException =
                new SubscriptionValidationException(notFoundSubscriptionException);

            _storageBrokerMock.Setup(broker =>
                    broker.SelectSourceSubscriptionByIdAsync(inputSubscriptionId))
                .ReturnsAsync(invalidSubscription);

            // act
            var actualSubscriptionTask = _subscriptionService.UpdateSubscriptionAsync(inputSubscription);

            // assert
            var actualException = await Assert.ThrowsAsync<SubscriptionValidationException>(() =>
                actualSubscriptionTask);

            Assert.Equal(expectedValidationException.Message, actualException.Message);
            Assert.IsType<NotFoundSubscriptionException>(actualException.InnerException);
            Assert.Equal(expectedValidationException.InnerException.Message, actualException.InnerException.Message);

            _storageBrokerMock.Verify(broker =>
                    broker.SelectSourceSubscriptionByIdAsync(inputSubscriptionId),
                Times.Once);

            _storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowValidationExceptionOnUpdateWhenInputSubscriptionIsNullAsync()
        {
            // arrange
            SourceSubscription nullInputSubscription = null;
            var nullSubscriptionException = new NullSubscriptionException();

            var expectedValidationException =
                new SubscriptionValidationException(nullSubscriptionException);

            // act
            var actualSubscriptionTask = _subscriptionService.UpdateSubscriptionAsync(nullInputSubscription);

            // assert
            var actualException = await Assert.ThrowsAsync<SubscriptionValidationException>(() =>
                actualSubscriptionTask);

            Assert.Equal(expectedValidationException.Message, actualException.Message);
            Assert.IsType<NullSubscriptionException>(actualException.InnerException);
            Assert.Equal(expectedValidationException.InnerException.Message, actualException.InnerException.Message);

            _storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowValidationExceptionOnDeleteWhenStorageSubscriptionIsNullAsync()
        {
            // arrange
            var randomSubscription = GetRandomSubscription();
            var inputSubscriptionId = randomSubscription.Id;
            SourceSubscription invalidSubscription = null;
            var notFoundSubscriptionException = new NotFoundSubscriptionException(inputSubscriptionId);

            var expectedValidationException =
                new SubscriptionValidationException(notFoundSubscriptionException);

            _storageBrokerMock.Setup(broker =>
                    broker.SelectSourceSubscriptionByIdAsync(inputSubscriptionId))
                .ReturnsAsync(invalidSubscription);

            // act
            var actualSubscriptionTask = _subscriptionService.DeleteSubscriptionByIdAsync(inputSubscriptionId);

            // assert
            var actualException = await Assert.ThrowsAsync<SubscriptionValidationException>(() =>
                actualSubscriptionTask);

            Assert.Equal(expectedValidationException.Message, actualException.Message);
            Assert.IsType<NotFoundSubscriptionException>(actualException.InnerException);
            Assert.Equal(expectedValidationException.InnerException.Message, actualException.InnerException.Message);

            _storageBrokerMock.Verify(broker =>
                    broker.SelectSourceSubscriptionByIdAsync(inputSubscriptionId),
                Times.Once);

            _storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowValidationExceptionOnAddWhenInputSubscriptionIsNull()
        {
            // arrange
            SourceSubscription nullInputSubscription = null;
            var nullSubscriptionException = new NullSubscriptionException();

            var expectedValidationException =
                new SubscriptionValidationException(nullSubscriptionException);

            // act
            var actualSubscriptionTask = _subscriptionService.AddSubscriptionAsync(nullInputSubscription);

            // assert
            var actualException = await Assert.ThrowsAsync<SubscriptionValidationException>(() =>
                actualSubscriptionTask);

            Assert.Equal(expectedValidationException.Message, actualException.Message);
            Assert.IsType<NullSubscriptionException>(actualException.InnerException);
            Assert.Equal(expectedValidationException.InnerException.Message, actualException.InnerException.Message);

            _storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}