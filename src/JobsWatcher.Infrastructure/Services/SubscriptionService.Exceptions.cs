using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Exceptions.Subscription;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SubscriptionService
    {
        private async Task<SourceSubscription> TryCatch(ReturningSubscriptionFunction returningSubscriptionFunction)
        {
            try
            {
                return await returningSubscriptionFunction();
            }
            catch (NotFoundSubscriptionException exception)
            {
                throw CreateAndLogValidationException(exception);
            }
            catch (NullSubscriptionException exception)
            {
                throw CreateAndLogValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private async Task<List<SourceSubscription>> TryCatch(
            ReturningListSubscriptionFunction returningListSubscriptionFunction)
        {
            try
            {
                return await returningListSubscriptionFunction();
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            var serviceException = new SubscriptionServiceException(exception);
            _logger.LogError(serviceException, serviceException.Message);

            return serviceException;
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            var validationException = new SubscriptionValidationException(exception);
            _logger.LogError(validationException, validationException.Message);

            return validationException;
        }

        private delegate Task<SourceSubscription> ReturningSubscriptionFunction();

        private delegate Task<List<SourceSubscription>> ReturningListSubscriptionFunction();
    }
}