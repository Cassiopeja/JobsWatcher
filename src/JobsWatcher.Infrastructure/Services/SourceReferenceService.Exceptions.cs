using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Exceptions.SourceReference;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SourceReferenceService
    {
        private async Task<SourceReference> TryCatch(ReturningSourceReferenceFunction returningSourceReferenceFunction)
        {
            try
            {
                return await returningSourceReferenceFunction();
            }
            catch (NotFoundSourceReferenceException exception)
            {
                throw CreateAndLogValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private async Task<IEnumerable<SourceReference>> TryCatch(
            Func<Task<IEnumerable<SourceReference>>> returningSourceReferencesFunction)
        {
            try
            {
                return await returningSourceReferencesFunction();
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            var serviceException = new SourceReferenceServiceException(exception);
            _logger.LogError(serviceException, serviceException.Message);

            return serviceException;
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            var validationException = new SourceReferenceValidationException(exception);
            _logger.LogError(validationException, validationException.Message);

            return validationException;
        }

        private delegate Task<SourceReference> ReturningSourceReferenceFunction();
    }
}