using System;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Exceptions.Vacancy;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class VacancyService
    {
        private async Task<Vacancy> TryCatch(ReturningVacancyFunction returningVacancyFunction)
        {
            try
            {
                return await returningVacancyFunction();
            }
            catch (NotFoundVacancyException notFoundVacancyException)
            {
                throw CreateAndLogValidationException(notFoundVacancyException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            var vacancyServiceException = new VacancyServiceException(exception);
            _logger.LogError(vacancyServiceException, vacancyServiceException.Message);

            return vacancyServiceException;
        }

        private Exception CreateAndLogValidationException(NotFoundVacancyException exception)
        {
            var vacancyValidationException = new VacancyValidationException(exception);
            _logger.LogError(vacancyValidationException, vacancyValidationException.Message);

            return vacancyValidationException;
        }

        private delegate Task<Vacancy> ReturningVacancyFunction();
    }
}