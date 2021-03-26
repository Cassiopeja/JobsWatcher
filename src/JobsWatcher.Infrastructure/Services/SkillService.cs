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
    public partial class SkillService : ISkillService
    {
        private readonly ILogger<SkillService> _logger;
        private readonly IStorageBroker _storageBroker;

        public SkillService(IStorageBroker storageBroker, ILogger<SkillService> logger)
        {
            _storageBroker = storageBroker;
            _logger = logger;
        }

        public Task<IList<Skill>> GetAllSkills(GetAllSkillsFilter filter = null)
        {
            return TryCatch(async () =>
            {
                if (filter?.SubscriptionId == null)
                {
                    var queryable = _storageBroker.SelectAllSkills();
                    queryable = AddFiltersOnQuery(filter, queryable);
                    return await queryable.ToListAsync();
                }

                //check subscriptionId
                var subscription = await _storageBroker.SelectSourceSubscriptionByIdAsync(filter.SubscriptionId.Value);
                ValidateSubscription(subscription, filter.SubscriptionId.Value);

                var queryableBySubscription = _storageBroker.SelectAllSubscriptionVacancies();
                var query = AddFiltersWithSubscriptionOnQuery(filter, queryableBySubscription);

                return await query.ToListAsync();
            });
        }

        private IQueryable<Skill> AddFiltersWithSubscriptionOnQuery(GetAllSkillsFilter filter,
            IQueryable<SubscriptionVacancy> queryable)
        {
            var query = queryable
                .Include(sv => sv.Vacancy)
                .ThenInclude(v => v.VacancySkills)
                .ThenInclude(vs => vs.Skill)
                .Where(sv => sv.SourceSubscriptionId == filter.SubscriptionId.Value)
                .SelectMany(sv => sv.Vacancy.VacancySkills)
                .Select(vs => vs.Skill)
                .Distinct();

            if (!string.IsNullOrEmpty(filter.SearchText))
                query = query.Where(s => s.Name.ToLower().StartsWith(filter.SearchText.ToLower()));

            if (filter.Limit != null) query = query.OrderBy(s => s.Name).Take(filter.Limit.Value);

            return query;
        }

        private IQueryable<Skill> AddFiltersOnQuery(GetAllSkillsFilter filter, IQueryable<Skill> queryable)
        {
            if (filter == null) return queryable;

            if (!string.IsNullOrEmpty(filter.SearchText))
                queryable = queryable.Where(s => s.Name.ToLower().StartsWith(filter.SearchText.ToLower()));
            if (filter.Limit != null) queryable = queryable.OrderBy(s => s.Name).Take(filter.Limit.Value);

            return queryable;
        }
    }
}