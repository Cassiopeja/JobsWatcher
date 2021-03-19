using System.Linq;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Extensions
{
    public static class StorageBrokerExtensions
    {
        public static IQueryable<Vacancy> IncludeAllProperties(this IQueryable<Vacancy> vacancies)
        {
            return vacancies
                .Include(x => x.Area)
                .Include(x => x.Currency)
                .Include(x => x.Employer)
                .Include(x => x.VacancySkills)
                .ThenInclude(s => s.Skill)
                .Include(x => x.SourceType)
                .Include(x => x.Employment)
                .Include(x => x.Schedule);
        }

        public static IQueryable<SourceSubscription> IncludeAllProperties(
            this IQueryable<SourceSubscription> subscriptions)
        {
            return subscriptions
                .Include(x => x.SourceType);
        }

        public static IQueryable<SourceReference> IncludeAllProperties(
            this IQueryable<SourceReference> sourceReferences)
        {
            return sourceReferences
                .Include(x => x.SourceType);
        }

        public static IQueryable<SubscriptionVacancy> IncludeAllProperties(
            this IQueryable<SubscriptionVacancy> subscriptionVacancies)
        {
            return subscriptionVacancies
                    .Include(sv => sv.Vacancy.Area)
                    .Include(x => x.Vacancy.Employer)
                    .Include(x => x.Vacancy.VacancySkills)
                    .ThenInclude(s => s.Skill)
                    .Include(x => x.Vacancy.SourceType)
                    .Include(x => x.Vacancy.Employment)
                    .Include(x => x.Vacancy.Schedule)
                    .Include(x => x.Vacancy.Currency);
        }
    }
}