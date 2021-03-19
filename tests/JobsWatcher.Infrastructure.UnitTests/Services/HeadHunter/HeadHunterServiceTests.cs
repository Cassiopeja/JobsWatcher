using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Bogus;
using Bogus.DataSets;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using JobsWatcher.Infrastructure.Brokers.HeadHunter;
using JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities;
using JobsWatcher.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Currency = JobsWatcher.Core.Entities.Currency;

namespace JobsWatcher.Infrastructure.UnitTests.Services.HeadHunter
{
    public partial class HeadHunterServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IHeadHunterBroker> _headHunterBrokerMock;
        private readonly SourceType _headHunterSourceType;
        private readonly Mock<ILogger<HeadHunterService>> _loggerMock;
        private readonly ISourceService _sourceService;
        private readonly Mock<IStorageBroker> _storageBrokerMock;
        private Faker<HeadHunterArea> _headHunterAreaFaker;
        private Faker<HeadHunterEmployer> _headHunterEmployerFaker;
        private Faker<HeadHunterEmployment> _headHunterEmploymentFaker;
        private Faker<HeadHunterSalary> _headHunterSalaryFaker;
        private Faker<HeadHunterSchedule> _headHunterScheduleFaker;
        private Faker<HeadHunterSkill> _headHunterSkillFaker;
        private Faker<HeadHunterVacancy> _headHunterVacancyFaker;
        private Faker<SourceArea> _sourceAreaFaker;
        private Faker<SourceEmployer> _sourceEmployerFaker;
        private Faker<SourceSubscription> _sourceSubscriptionFaker;
        private Faker<Vacancy> _vacancyFaker;

        public HeadHunterServiceTests()
        {
            _fixture = new Fixture();
            _storageBrokerMock = new Mock<IStorageBroker>();
            _headHunterBrokerMock = new Mock<IHeadHunterBroker>();
            _loggerMock = new Mock<ILogger<HeadHunterService>>();
            _sourceService = new HeadHunterService(
                _storageBrokerMock.Object,
                _headHunterBrokerMock.Object,
                _loggerMock.Object);
            _headHunterSourceType = new SourceType {Id = 1, Name = "HeadHunter"};
            var lorem = new Lorem("ru");
            SetupHeadHunterFakers(lorem);
            SetupVacancyFakers(lorem);
        }

        private void SetupVacancyFakers(Lorem lorem)
        {
            _vacancyFaker = new Faker<Vacancy>()
                .RuleFor(v => v.Id, f => f.Random.Number(1, 10))
                .RuleFor(v => v.Title, f => lorem.Sentence())
                .RuleFor(v => v.Descriptions, f => lorem.Text())
                .RuleFor(v => v.Responsibilities, f => lorem.Sentence())
                .RuleFor(v => v.Url, f => f.Internet.Url())
                .RuleFor(v => v.SourceCreatedDate, f => f.Date.Past())
                .RuleFor(v => v.SourceUpdatedDate, (f, v) => v.SourceCreatedDate)
                .RuleFor(v => v.ContentUpdatedDate, (f, v) => v.SourceCreatedDate);
            _sourceEmployerFaker = new Faker<SourceEmployer>()
                .RuleFor(e => e.Id, f => f.Random.Number(1, 10))
                .RuleFor(e => e.SourceId, f => f.Random.Number(1, 10).ToString())
                .RuleFor(e => e.EmployerId, f => f.Random.Number(1, 10))
                .RuleFor(e => e.Url, f => f.Internet.Url())
                .RuleFor(e => e.Employer, (f, e) => new Employer {Id = e.EmployerId, Name = lorem.Sentence()});
            _sourceAreaFaker = new Faker<SourceArea>()
                .RuleFor(a => a.Id, f => f.IndexFaker)
                .RuleFor(a => a.AreaId, f => f.Random.Number(1, 10))
                .RuleFor(a => a.SourceId, f => f.Random.Number(1, 10).ToString())
                .RuleFor(a => a.Area, (f, a) => new Area {Id = a.AreaId, Name = lorem.Word()});
            _sourceSubscriptionFaker = new Faker<SourceSubscription>()
                .RuleFor(s => s.Id, f => f.IndexFaker)
                .RuleFor(s => s.Name, f => lorem.Word())
                .RuleFor(s => s.CreatedDate, f => DateTimeOffset.Now)
                .RuleFor(s => s.UpdateDate, f => DateTimeOffset.Now)
                .RuleFor(s => s.Parameters, f => "{}")
                .RuleFor(s => s.SourceType, f => new SourceType {Id = 1, Name = "Test"});
        }

        private void SetupHeadHunterFakers(Lorem lorem)
        {
            _headHunterAreaFaker = new Faker<HeadHunterArea>()
                .RuleFor(a => a.Id, f => f.Random.Number(1, 10).ToString())
                .RuleFor(a => a.Name, f => f.Address.City());
            _headHunterEmployerFaker = new Faker<HeadHunterEmployer>()
                .RuleFor(e => e.Id, f => f.Random.Number(1, 10).ToString())
                .RuleFor(e => e.Name, f => f.Company.CompanyName())
                .RuleFor(e => e.Url, f => f.Internet.Url());
            _headHunterSalaryFaker = new Faker<HeadHunterSalary>()
                .RuleFor(s => s.Currency, f => f.Finance.Currency().Code)
                .RuleFor(s => s.From, f => f.Random.Number(1, 10))
                .RuleFor(s => s.To, f => f.Random.Number(1, 10))
                .RuleFor(s => s.IsGross, f => f.Random.Bool());
            _headHunterSkillFaker = new Faker<HeadHunterSkill>()
                .RuleFor(s => s.Name, f => lorem.Word());
            _headHunterScheduleFaker = new Faker<HeadHunterSchedule>()
                .RuleFor(s => s.Id, f => lorem.Word())
                .RuleFor(s => s.Name, f => lorem.Sentence());
            _headHunterEmploymentFaker = new Faker<HeadHunterEmployment>()
                .RuleFor(s => s.Id, f => lorem.Word())
                .RuleFor(s => s.Name, f => lorem.Sentence());
            _headHunterVacancyFaker = new Faker<HeadHunterVacancy>()
                .RuleFor(v => v.Id, f => f.Random.Number(1, 10).ToString())
                .RuleFor(v => v.Area, f => _headHunterAreaFaker.Generate())
                .RuleFor(v => v.Employer, f => _headHunterEmployerFaker.Generate())
                .RuleFor(v => v.Description, f => lorem.Text())
                .RuleFor(v => v.Name, f => lorem.Sentence())
                .RuleFor(v => v.Salary, f => _headHunterSalaryFaker.Generate())
                .RuleFor(v => v.Url, f => f.Internet.Url())
                .RuleFor(v => v.IsPremium, f => f.Random.Bool())
                .RuleFor(v => v.CreatedDate, f => f.Date.Past())
                .RuleFor(v => v.UpdateDate, (f, v) => v.CreatedDate)
                .RuleFor(v => v.RawData, f => lorem.Text())
                .RuleFor(v => v.KeySkills, f => new List<HeadHunterSkill> {_headHunterSkillFaker.Generate()})
                .RuleFor(v => v.Schedule, f => _headHunterScheduleFaker.Generate())
                .RuleFor(v => v.Employment, f => _headHunterEmploymentFaker.Generate());
        }

        private HeadHunterSnippet GetRandomHeadHunterSnippet()
        {
            return _fixture.Create<HeadHunterSnippet>();
        }

        private HeadHunterVacancy GetRandomHeadHunterVacancy()
        {
            return _headHunterVacancyFaker.Generate();
        }

        private Vacancy GetRandomVacancy()
        {
            return _vacancyFaker.Generate();
        }

        private SourceSubscription GetRandomSourceSubscription()
        {
            return _sourceSubscriptionFaker.Generate();
        }

        private async IAsyncEnumerable<HeadHunterSnippet> GetHeadHunterSnippetsAsync(List<HeadHunterSnippet> snippets)
        {
            foreach (var snippet in snippets) yield return snippet;
            await Task.CompletedTask;
        }

        private SourceEmployer GetSourceEmployer(Employer employer, HeadHunterVacancy headHunterVacancy,
            SourceType sourceType)
        {
            return new()
            {
                EmployerId = employer.Id,
                SourceId = headHunterVacancy.Employer.Id,
                SourceTypeId = sourceType.Id,
                Employer = employer
            };
        }

        private SourceArea GetSourceArea(Area returnedArea, HeadHunterVacancy vacancyResponse,
            SourceType returnedSourceType)
        {
            return new()
            {
                Id = 1,
                AreaId = returnedArea.Id,
                Area = returnedArea,
                SourceId = vacancyResponse.Area.Id,
                SourceTypeId = returnedSourceType.Id,
                SourceType = returnedSourceType
            };
        }

        private Currency GetCurrency(HeadHunterVacancy headHunterVacancy)
        {
            return new() {Id = 1, Name = headHunterVacancy.Salary.Currency};
        }

        private Schedule GetSchedule(HeadHunterVacancy headHunterVacancyResponse)
        {
            return new()
                {Id = 1, Code = headHunterVacancyResponse.Schedule.Id, Name = headHunterVacancyResponse.Schedule.Name};
        }

        private Employment GetEmployment(HeadHunterVacancy headHunterVacancyResponse)
        {
            var returnedEmployment = new Employment
            {
                Id = 1, Code = headHunterVacancyResponse.Employment.Id, Name = headHunterVacancyResponse.Employment.Name
            };
            return returnedEmployment;
        }

        private Vacancy GetOlderVersionSameVacancy(HeadHunterVacancy headHunterVacancy,
            HeadHunterSnippet headHunterSnippet)
        {
            var vacancy = GetVacancy(headHunterVacancy, headHunterSnippet);
            vacancy.SourceUpdatedDate = vacancy.SourceUpdatedDate.AddDays(-1);
            return vacancy;
        }

        private Vacancy GetVacancy(HeadHunterVacancy headHunterVacancy,
            HeadHunterSnippet headHunterSnippet)
        {
            var vacancy = GetRandomVacancy();
            vacancy.SourceId = headHunterVacancy.Id;
            vacancy.SourceUpdatedDate = headHunterSnippet.UpdateDate;
            vacancy.IsArchived = false;
            return vacancy;
        }
    }
}