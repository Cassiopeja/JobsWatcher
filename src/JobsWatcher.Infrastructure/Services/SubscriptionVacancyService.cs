using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using JobsWatcher.Infrastructure.Extensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobsWatcher.Infrastructure.Services
{
    public partial class SubscriptionVacancyService : ISubscriptionVacancyService
    {
        private readonly ILogger<SubscriptionVacancyService> _logger;
        private readonly IStorageBroker _storageBroker;

        public SubscriptionVacancyService(IStorageBroker storageBroker, ILogger<SubscriptionVacancyService> logger)
        {
            _storageBroker = storageBroker;
            _logger = logger;
        }

        public Task<PagedItems<SubscriptionVacancy>> GetSubscriptionVacanciesAsync(int subscriptionId,
            GetAllSubscriptionVacanciesFilter filter = null,
            PaginationFilter paginationFilter = null, SortByOptions sortByOptions = null)
        {
            return TryCatch(async () =>
            {
                var subscription = await _storageBroker.SelectSourceSubscriptionByIdAsync(subscriptionId);
                ValidateSubscription(subscription, subscriptionId);
                var queryable = _storageBroker.SelectAllSubscriptionVacancies();

                queryable = ApplySorting(sortByOptions, queryable).IncludeAllProperties()
                    .Where(sv => sv.SourceSubscriptionId == subscriptionId);
                queryable = AddFiltersOnQuery(filter, queryable);

                if (paginationFilter == null)
                    return await queryable.ToPagedItemsAsync();

                return await queryable.ToPagedItemsAsync(paginationFilter.PageNumber, paginationFilter.PageSize);
            });
        }

        public Task<SubscriptionVacancy> GetSubscriptionVacancyByIdAsync(int subscriptionVacancyId)
        {
            return TryCatch(async () =>
            {
                var subscriptionVacancy = await _storageBroker.SelectAllSubscriptionVacancies()
                    .IncludeAllProperties().FirstOrDefaultAsync(sv => sv.Id == subscriptionVacancyId);
                ValidateStorageSubscriptionVacancy(subscriptionVacancy, subscriptionVacancyId);
                return subscriptionVacancy;
            });
        }

        public Task<SubscriptionVacancy> AddSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy)
        {
            return TryCatch(async () =>
            {
                ValidateSubscriptionVacancyOnAdd(subscriptionVacancy);
                subscriptionVacancy.Id = 0;
                return await _storageBroker.InsertSubscriptionVacancyAsync(subscriptionVacancy);
            });
        }

        public Task<SubscriptionVacancy> UpdateSubscriptionVacancyAsync(SubscriptionVacancy subscriptionVacancy)
        {
            return TryCatch(async () =>
            {
                ValidateSubscriptionVacancyOnUpdate(subscriptionVacancy);
                var storageSubscriptionVacancy =
                    await _storageBroker.UpdateSubscriptionVacancyAsync(subscriptionVacancy);
                ValidateStorageSubscriptionVacancy(storageSubscriptionVacancy, subscriptionVacancy.Id);
                return await GetSubscriptionVacancyByIdAsync(subscriptionVacancy.Id);
            });
        }

        public Task<SubscriptionVacancy> DeleteSubscriptionVacancyByIdAsync(int subscriptionVacancyId)
        {
            return TryCatch(async () =>
            {
                var storageSubscriptionVacancy =
                    await _storageBroker.SelectSubscriptionVacancyByIdAsync(subscriptionVacancyId);
                ValidateStorageSubscriptionVacancy(storageSubscriptionVacancy, subscriptionVacancyId);
                return await _storageBroker.DeleteSubscriptionVacancyAsync(storageSubscriptionVacancy);
            });
        }

        public Task<List<SubscriptionVacancy>> GetSimilarSubscriptionVacancies(int subscriptionVacancyId)
        {
            return TryCatch(async () =>
            {
                var storageSubscriptionVacancy =
                    await _storageBroker.SelectSubscriptionVacancyByIdAsync(subscriptionVacancyId);
                ValidateStorageSubscriptionVacancy(storageSubscriptionVacancy, subscriptionVacancyId);
                var similarVacancies = await _storageBroker.SelectAllSubscriptionVacancies().IncludeAllProperties()
                    .Where(sv =>
                        sv.SourceSubscriptionId == storageSubscriptionVacancy.SourceSubscriptionId
                        && sv.Vacancy.EmployerId == storageSubscriptionVacancy.Vacancy.EmployerId
                        && sv.Vacancy.HashCode == storageSubscriptionVacancy.Vacancy.HashCode
                        && sv.Id != storageSubscriptionVacancy.Id).ToListAsync();
                return similarVacancies;
            });
        }

        public Task<PagedItems<SubscriptionVacancy>> GetSubscriptionVacanciesGroupByCompanyAsync(int subscriptionId,
            GetAllSubscriptionVacanciesFilter filter = null,
            PaginationFilter paginationFilter = null)
        {
            return TryCatch(async () =>
            {
                var subscription = await _storageBroker.SelectSourceSubscriptionByIdAsync(subscriptionId);
                ValidateSubscription(subscription, subscriptionId);
                var queryable = _storageBroker.SelectAllSubscriptionVacancies()
                    .Where(sv => sv.SourceSubscriptionId == subscriptionId);
                queryable = AddFiltersOnQuery(filter, queryable);
                var employers = queryable.Select(sv => sv.Vacancy.Employer)
                    .Distinct()
                    .OrderBy(employer => employer.Name)
                    .Select(employer => employer.Id);
                var pagedEmployers = await employers.ToPagedItemsAsync(paginationFilter);
                var vacancies = _storageBroker.SelectAllSubscriptionVacancies()
                    .IncludeAllProperties()
                    .Where(sv => sv.SourceSubscriptionId == subscriptionId)
                    .Where(sv=> pagedEmployers.Data.Contains(sv.Vacancy.Employer.Id));
                vacancies = AddFiltersOnQuery(filter, vacancies);

                var vacanciesList = await vacancies.ToListAsync();
                return new PagedItems<SubscriptionVacancy>(vacanciesList, pagedEmployers.TotalCount, 
                    pagedEmployers.PageNumber, pagedEmployers.PageSize);
            });
        }

        private IQueryable<SubscriptionVacancy> ApplySorting(SortByOptions sortByOptions,
            IQueryable<SubscriptionVacancy> queryable)
        {
            if (sortByOptions == null || string.IsNullOrEmpty(sortByOptions.SortBy)) return queryable;

            queryable = (sortByOptions.SortBy, sortByOptions.Ascending) switch
            {
                ("Vacancy.SourceCreatedDate", true) => queryable.OrderBy(sv => sv.Vacancy.SourceCreatedDate),
                ("Vacancy.SourceCreatedDate", false) => queryable.OrderByDescending(sv => sv.Vacancy.SourceCreatedDate),
                ("Vacancy.ContentUpdatedDate", true) => queryable.OrderBy(sv => sv.Vacancy.ContentUpdatedDate),
                ("Vacancy.ContentUpdatedDate", false) => queryable.OrderByDescending(
                    sv => sv.Vacancy.ContentUpdatedDate),
                ("Rating", true) => queryable.OrderBy(sv => sv.Rating),
                ("Rating", false) => queryable.OrderByDescending(sv => sv.Rating),
                _ => queryable
            };

            return queryable;
        }

        private IQueryable<SubscriptionVacancy> AddFiltersOnQuery(GetAllSubscriptionVacanciesFilter filter,
            IQueryable<SubscriptionVacancy> queryable)
        {
            if (filter == null) return queryable;

            if (!string.IsNullOrEmpty(filter.SearchText))
                queryable = queryable.Where(sv =>
                    EF.Functions.Like(sv.Vacancy.Descriptions.ToLower(), '%' + filter.SearchText.ToLower() + "%"));

            if (filter.AreaIds != null && filter.AreaIds.Length > 0)
            {
                var predicate = PredicateBuilder.New<SubscriptionVacancy>();
                foreach (var areaId in filter.AreaIds) predicate = predicate.Or(p => p.Vacancy.AreaId == areaId);

                queryable = queryable.Where(predicate);
            }

            if (filter.EmploymentIds != null && filter.EmploymentIds.Length > 0)
            {
                var predicate = PredicateBuilder.New<SubscriptionVacancy>();
                foreach (var employmentId in filter.EmploymentIds)
                    predicate = predicate.Or(p => p.Vacancy.EmploymentId == employmentId);

                queryable = queryable.Where(predicate);
            }

            if (filter.EmployerIds != null && filter.EmployerIds.Length > 0)
            {
                var predicate = PredicateBuilder.New<SubscriptionVacancy>();
                foreach (var employerId in filter.EmployerIds)
                    predicate = predicate.Or(p => p.Vacancy.EmployerId == employerId);

                queryable = queryable.Where(predicate);
            }

            if (filter.ScheduleIds != null && filter.ScheduleIds.Length > 0)
            {
                var predicate = PredicateBuilder.New<SubscriptionVacancy>();
                foreach (var scheduleId in filter.ScheduleIds)
                    predicate = predicate.Or(p => p.Vacancy.ScheduleId == scheduleId);

                queryable = queryable.Where(predicate);
            }

            if (filter.Ratings != null && filter.Ratings.Length > 0)
            {
                var predicate = PredicateBuilder.New<SubscriptionVacancy>();
                foreach (var rating in filter.Ratings) predicate = predicate.Or(p => p.Rating == rating);

                queryable = queryable.Where(predicate);
            }

            if (filter.SkillIds != null && filter.SkillIds.Length > 0)
            {
                var predicate = PredicateBuilder.New<SubscriptionVacancy>();
                foreach (var skillId in filter.SkillIds)
                    predicate = predicate.Or(p => p.Vacancy.VacancySkills.Any(s => s.SkillId == skillId));

                queryable = queryable.Where(predicate);
            }

            if (filter.IsHidden != null) queryable = queryable.Where(sv => sv.IsHidden == filter.IsHidden);

            if (filter.IsArchived != null)
                queryable = queryable.Where(sv => sv.Vacancy.IsArchived == filter.IsArchived);

            return queryable;
        }
    }
}