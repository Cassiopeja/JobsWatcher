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
    public class EmployerService : IEmployerService
    {
        private readonly IStorageBroker _storageBroker;
        private readonly ILogger<EmployerService> _logger;

        public EmployerService(IStorageBroker storageBroker, ILogger<EmployerService> logger)
        {
            _storageBroker = storageBroker;
            _logger = logger;
        }
        
        public async Task<List<Employer>> GetEmployersAsync()
        {
            return await _storageBroker.SelectAllEmployers().ToListAsync();
        }

        public async Task<List<Employer>> GetEmployersBySubscriptionIdAsync(int subscriptionId)
        {
            return await _storageBroker
                .SelectAllSubscriptionVacancies()
                .Where(sv => sv.SourceSubscriptionId == subscriptionId)
                .Select(sv =>
                    new Employer
                    {
                        Id = sv.Vacancy.Employer.Id,
                        Name = sv.Vacancy.Employer.Name
                    }
                )
                .Distinct()
                .ToListAsync();
        }
    }
}