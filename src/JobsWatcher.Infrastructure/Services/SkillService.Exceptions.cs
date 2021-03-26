using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Exceptions.Skill;
using JobsWatcher.Core.Exceptions.Subscription;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SkillService
    {
        private async Task<IList<Skill>> TryCatch(Func<Task<IList<Skill>>> returningSkillsFunction)
        {
            try
            {
                return await returningSkillsFunction();
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

        private Exception CreateAndLogServiceException(Exception exception)
        {
            var validationException = new SkillValidationException(exception);
            _logger.LogError(validationException, validationException.Message);

            return validationException;
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            var serviceException = new SkillServiceException(exception);
            _logger.LogError(serviceException, serviceException.Message);

            return serviceException;
        }
    }
}