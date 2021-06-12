using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Exceptions.Area;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class AreaService
    {
        private async Task<List<Area>> TryCatch(Func<Task<List<Area>>> returningAreasFunction)
        {
            try
            {
                return await returningAreasFunction();
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private Exception CreateAndLogServiceException(Exception exception)
        {
            var serviceException = new AreaServiceException(exception);
            _logger.LogError(serviceException, serviceException.Message);

            return serviceException;
        }
    }
}