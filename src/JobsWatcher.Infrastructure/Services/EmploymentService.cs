using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public class EmploymentService : IEmploymentService
    {
        private readonly ILogger<EmploymentService> _logger;
        private readonly IStorageBroker _storageBroker;

        public EmploymentService(IStorageBroker storageBroker, ILogger<EmploymentService> logger)
        {
            _storageBroker = storageBroker;
            _logger = logger;
        }

        public async Task<List<Employment>> GetEmploymentsAsync()
        {
            return await _storageBroker.SelectAllEmployments().ToListAsync();
        }

        public async Task<List<Employment>> GetEmploymentsBySubscriptionIdAsync(int subscriptionId)
        {
            return await _storageBroker
                .SelectAllSubscriptionVacancies()
                .Where(sv => sv.SourceSubscriptionId == subscriptionId 
                             && sv.Vacancy.EmploymentId != null)
                .Select(sv =>
                    new Employment
                    {
                        Id = sv.Vacancy.Employment.Id,
                        Name = sv.Vacancy.Employment.Name
                    }
                )
                .Distinct()
                .ToListAsync();
        }
    }
}