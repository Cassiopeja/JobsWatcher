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
    public class AreaService : IAreaService
    {
        private readonly ILogger<AreaService> _logger;
        private readonly IStorageBroker _storageBroker;

        public AreaService(IStorageBroker storageBroker, ILogger<AreaService> logger)
        {
            _storageBroker = storageBroker;
            _logger = logger;
        }

        public async Task<List<Area>> GetAreasAsync()
        {
            return await _storageBroker.SelectAllAreas().ToListAsync();
        }

        public async Task<List<Area>> GetAreasBySubscriptionIdAsync(int subscriptionId)
        {
            return await _storageBroker
                .SelectAllSubscriptionVacancies()
                .Where(sv => sv.SourceSubscriptionId == subscriptionId 
                             && sv.Vacancy.AreaId != null)
                .Select(sv => 
                    new Area
                    {
                        Id = sv.Vacancy.Area.Id, 
                        Name = sv.Vacancy.Area.Name
                    })
                .Distinct()
                .ToListAsync();
        }
    }
}