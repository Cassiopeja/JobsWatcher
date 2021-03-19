using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Exceptions.Subscription;
using JobsWatcher.Core.Exceptions.SubscriptionVacancy;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SubscriptionVacancyService
    {
        private async Task<SubscriptionVacancy> TryCatch(
            Func<Task<SubscriptionVacancy>> returningSubscriptionVacancyFunc)
        {
            try
            {
                return await returningSubscriptionVacancyFunc();
            }
            catch (NotFoundSubscriptionVacancyException exception)
            {
                throw CreateAndLogValidationException(exception);
            }
            catch (NullSubscriptionVacancyException exception)
            {
                throw CreateAndLogValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private async Task<PagedItems<SubscriptionVacancy>> TryCatch(
            Func<Task<PagedItems<SubscriptionVacancy>>> returningSubscriptionVacanciesFunc)
        {
            try
            {
                return await returningSubscriptionVacanciesFunc();
            }
            catch (NotFoundSubscriptionException exception)
            {
                throw CreateAndLogValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }
        private async Task<List<SubscriptionVacancy>> TryCatch(
            Func<Task<List<SubscriptionVacancy>>> returningSubscriptionVacanciesFunc)
        {
            try
            {
                return await returningSubscriptionVacanciesFunc();
            }
            catch (NotFoundSubscriptionException exception)
            {
                throw CreateAndLogValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }
        
        private Exception CreateAndLogServiceException(Exception exception)
        {
            var serviceException = new SubscriptionVacancyServiceException(exception);
            _logger.LogError(serviceException, serviceException.Message);

            return serviceException;
        }
        
        private Exception CreateAndLogValidationException(Exception exception)
        {
            var validationException = new SubscriptionVacancyValidationException(exception);
            _logger.LogError(validationException, validationException.Message);

            return validationException;
        }
    }
}