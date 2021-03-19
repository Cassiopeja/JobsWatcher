using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Infrastructure.Brokers.StorageBroker;
using Currency = JobsWatcher.Core.Entities.Currency;

namespace JobsWatcher.Api.IntegrationTests.Helpers
{
    public static class Utilities
    {
        private static Faker<SourceSubscription> _sourceSubscriptionFaker;
        private static Faker<SubscriptionCreateDto> _sourceSubscriptionCreateDtoFaker;
        private static Faker<SubscriptionVacancy> _subscriptionVacancyFaker;
        private static Faker<Vacancy> _vacancyFaker;
        private static Faker<Area> _areaFaker;
        private static Faker<Employer> _employerFaker;
        private static Faker<Currency> _currencyFaker;
        private static Faker<Schedule> _scheduleFaker;
        private static Faker<Employment> _employmentFaker;

        static Utilities()
        {
            SetupFakers();
        }

        private static void SetupFakers()
        {
            var lorem = new Lorem("ru");
            SetupSubscriptionFakers(lorem);
            SetupVacancyFakers(lorem);
        }

        private static void SetupVacancyFakers(Lorem lorem)
        {
            _areaFaker = new Faker<Area>()
                .RuleFor(a => a.Id, 0)
                .RuleFor(a => a.Name, () => lorem.Word());
            _employerFaker = new Faker<Employer>()
                .RuleFor(e => e.Id, 0)
                .RuleFor(e => e.Name, () => lorem.Sentence());
            _employmentFaker = new Faker<Employment>()
                .RuleFor(e => e.Id, 0)
                .RuleFor(e => e.Name, () => lorem.Word());
            _scheduleFaker = new Faker<Schedule>()
                .RuleFor(s => s.Id, 0)
                .RuleFor(s => s.Name, () => lorem.Word());
            _currencyFaker = new Faker<Currency>()
                .RuleFor(c => c.Id, 0)
                .RuleFor(c => c.Name, faker => faker.Finance.Currency().Code);
            _vacancyFaker = new Faker<Vacancy>()
                .RuleFor(v => v.Id, 0)
                .RuleFor(v => v.SourceTypeId, 1)
                .RuleFor(v => v.Title, () => lorem.Sentence())
                .RuleFor(v => v.Descriptions, () => lorem.Text())
                .RuleFor(v => v.Responsibilities, () => lorem.Sentence())
                .RuleFor(v => v.Url, f => f.Internet.Url())
                .RuleFor(v => v.Area, () => _areaFaker.Generate())
                .RuleFor(v => v.Currency, () => _currencyFaker.Generate())
                .RuleFor(v => v.Employer, () => _employerFaker.Generate())
                .RuleFor(v => v.Employment, () => _employmentFaker.Generate())
                .RuleFor(v => v.Schedule, () => _scheduleFaker.Generate())
                .RuleFor(v => v.SourceCreatedDate, f => f.Date.Past())
                .RuleFor(v => v.SourceUpdatedDate, (f, v) => v.SourceCreatedDate)
                .RuleFor(v => v.ContentUpdatedDate, (f, v) => v.SourceCreatedDate);
        }

        private static void SetupSubscriptionFakers(Lorem lorem)
        {
            _sourceSubscriptionFaker = new Faker<SourceSubscription>()
                .RuleFor(s => s.Id, f => 0)
                .RuleFor(s => s.Name, f => lorem.Word())
                .RuleFor(s => s.CreatedDate, f => DateTimeOffset.Now)
                .RuleFor(s => s.UpdateDate, f => DateTimeOffset.Now)
                .RuleFor(s => s.Parameters, f => "{}");
            _sourceSubscriptionCreateDtoFaker = new Faker<SubscriptionCreateDto>()
                .RuleFor(s => s.Name, f => lorem.Word())
                .RuleFor(s => s.Parameters, f => "{}")
                .RuleFor(s => s.SourceTypeId, f => 1);
            _subscriptionVacancyFaker = new Faker<SubscriptionVacancy>()
                .RuleFor(s => s.Id, f => 0)
                .RuleFor(s => s.Comment, f => lorem.Paragraph())
                .RuleFor(s => s.Rating, f => f.Random.Int(0, 5))
                .RuleFor(s => s.IsHidden, f => f.Random.Bool())
                .RuleFor(s => s.Vacancy, f => _vacancyFaker.Generate());
        }

        public static SubscriptionCreateDto GetRandomSubscriptionCreateDto()
        {
            return _sourceSubscriptionCreateDtoFaker.Generate();
        }

        public static void InitializeDbForTests(StorageBroker db)
        {
            db.SourceTypes.Add(GetSeedingSourceType());
            db.SaveChanges();
            var sourceType = db.SourceTypes.Single();
            db.SourceSubscriptions.AddRange(GetSeedingSubscriptions(sourceType));
            db.SaveChanges();
            var subscription = db.SourceSubscriptions.Single(s => s.Id == 1);
            db.SubscriptionVacancies.AddRange(GetSeedingSubscriptionVacancies(subscription));
            db.SaveChanges();
        }

        private static IEnumerable<SourceSubscription> GetSeedingSubscriptions(SourceType sourceType)
        {
            var subscriptions = _sourceSubscriptionFaker.Generate(5);
            foreach (var subscription in subscriptions) subscription.SourceTypeId = sourceType.Id;

            return subscriptions;
        }

        private static IEnumerable<SubscriptionVacancy> GetSeedingSubscriptionVacancies(SourceSubscription subscription)
        {
            var subscriptionVacancies = _subscriptionVacancyFaker.Generate(25);
            subscriptionVacancies.ForEach(sv => sv.SourceSubscriptionId = subscription.Id);
            return subscriptionVacancies;
        }

        private static SourceType GetSeedingSourceType()
        {
            return new() {Id = 1, Name = "Head Hunter"};
        }
    }
}