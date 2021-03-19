using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using JobsWatcher.Infrastructure.Brokers.HeadHunter;
using JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities;
using JobsWatcher.Infrastructure.Brokers.HeadHunter.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JobsWatcher.Infrastructure.Services
{
    public class HeadHunterService : ISourceService
    {
        private const string ServiceCode = "HeadHunter";
        private readonly IHeadHunterBroker _headHunterBroker;
        private readonly ILogger<HeadHunterService> _logger;
        private readonly IStorageBroker _storageBroker;

        public HeadHunterService(IStorageBroker storageBroker, IHeadHunterBroker headHunterBroker,
            ILogger<HeadHunterService> logger)
        {
            _storageBroker = storageBroker;
            _headHunterBroker = headHunterBroker;
            _logger = logger;
        }

        public async Task UpdateAllSubscriptions()
        {
            var subscriptions = await GetSubscriptionsAsync();
            foreach (var subscription in subscriptions)
            {
                await UpdateSubscriptionVacancies(subscription);
            }
        }

        public async Task UpdateSingleSubscription(int subscriptionId)
        {
            var subscription = await _storageBroker.SelectAllSourceSubscriptions()
                .Include(s=>s.SourceType)
                .FirstOrDefaultAsync(s => s.Id == subscriptionId);
            if (subscription == null)
            {
                _logger.LogError("Subscription with id {subscriptionId} is not found", subscriptionId);
                return;
            }

            await UpdateSubscriptionVacancies(subscription);
        }
        

        public async Task UpdateArchivedAsync()
        {
            foreach (var vacancy in _storageBroker.SelectAllVacancies().Where(v => !v.IsArchived).ToList())
                try
                {
                    var headHunterVacancy = await _headHunterBroker.GetVacancy(vacancy.SourceId);
                    if (headHunterVacancy.IsArchived != vacancy.IsArchived) await ArchiveVacancy(vacancy);
                }
                catch (VacancyNotFound)
                {
                    await ArchiveVacancy(vacancy);
                }
        }

        private async Task ArchiveVacancy(Vacancy vacancy)
        {
            vacancy.IsArchived = true;
            await _storageBroker.UpdateVacancyAsync(vacancy);
            _logger.Log(LogLevel.Information, "Vacancy #{VacancyId} is archived", vacancy.Id);
        }

        private HeadHunterSubscriptionParameters GetSubscriptionParameters(string jsonParameters)
        {
            var subscriptionParameters =
                JsonConvert.DeserializeObject<HeadHunterSubscriptionParameters>(jsonParameters);
            return subscriptionParameters;
        }

        private async Task UpdateSubscriptionVacancies(SourceSubscription subscription)
        {
            _logger.LogInformation("Processing subscription with id {SubscriptionId}",
                subscription.Id);
            var sourceType = subscription.SourceType;
            var subscriptionParameters = GetSubscriptionParameters(subscription.Parameters);
            var snippets = _headHunterBroker.GetSnippets(subscriptionParameters);

            // for each subscription load vacancies
            var count = 0;
            await foreach (var snippet in snippets)
            {
                _logger.Log(LogLevel.Information, "Processing vacancy #{VacancyOderNumber}", count);
                var existedVacancy = await _storageBroker.SelectVacancyBySourceIdAsync(sourceType.Id, snippet.Id);
                if (existedVacancy == null)
                {
                    _logger.Log(LogLevel.Information, "Adding vacancy #{VacancyOderNumber}", count);
                    var headHunterVacancy = await _headHunterBroker.GetVacancy(snippet.Id);
                    var vacancy = await CreateVacancyAsync(sourceType, snippet, headHunterVacancy);
                    vacancy = await _storageBroker.InsertVacancyAsync(vacancy);
                    var vacancySkills = await CreateVacancySkillsAsync(vacancy, headHunterVacancy.KeySkills);
                    await SaveVacancySkillsAsync(vacancySkills);
                    await SaveSubscriptionVacancy(vacancy, subscription);
                }
                else
                {
                    if (existedVacancy.SourceUpdatedDate < snippet.UpdateDate ||
                        existedVacancy.IsArchived != snippet.Archived)
                    {
                        _logger.Log(LogLevel.Information, "Updating vacancy #{VacancyOderNumber}", count);
                        var headHunterVacancy = await _headHunterBroker.GetVacancy(snippet.Id);
                        var updatedVacancy = await CreateVacancyAsync(sourceType, snippet, headHunterVacancy);
                        updatedVacancy.Id = existedVacancy.Id;
                        if (!existedVacancy.HasContentChanges(updatedVacancy))
                            updatedVacancy.ContentUpdatedDate = existedVacancy.ContentUpdatedDate;
                        await _storageBroker.UpdateVacancyAsync(updatedVacancy);
                    }

                    var subscriptionVacancy = await
                        _storageBroker.SelectSubscriptionVacancyByVacancyAndSubscriptionIdAsync(subscription.Id,
                            existedVacancy.Id);
                    if (subscriptionVacancy == null) await SaveSubscriptionVacancy(existedVacancy, subscription);
                }

                count++;
            }
            _logger.LogInformation("Subscription with id {SubscriptionId} was updated",
                             subscription.Id);
        }

        private async Task SaveSubscriptionVacancy(Vacancy vacancy, SourceSubscription subscription)
        {
            var subscriptionVacancy = new SubscriptionVacancy
            {
                SourceSubscriptionId = subscription.Id,
                VacancyId = vacancy.Id
            };
            await _storageBroker.InsertSubscriptionVacancyAsync(subscriptionVacancy);
            _logger.Log(LogLevel.Information, "Saved subscription vacancy {VacancyId} for subscription {SubscriptionId}",
                vacancy.Id, subscription.Id);
        }

        private async Task<SourceSubscription[]> GetSubscriptionsAsync()
        {
            return await _storageBroker.SelectAllSourceSubscriptions().Include(s => s.SourceType).ToArrayAsync();
        }

        private async Task<Vacancy> CreateVacancyAsync(SourceType sourceType, HeadHunterSnippet snippet,
            HeadHunterVacancy headHunterVacancy)
        {
            var currencyName = headHunterVacancy.Salary?.Currency?.Trim();
            var vacancy = CreateVacancy(snippet, headHunterVacancy);
            vacancy.EmployerId = (await GetOrCreateEmployerAsync(sourceType, headHunterVacancy)).Id;
            vacancy.AreaId = (await GetOrCreateAreaAsync(sourceType, headHunterVacancy))?.Id;
            vacancy.CurrencyId = currencyName == null ? null : (await GetOrCreateCurrencyAsync(currencyName)).Id;
            vacancy.SourceTypeId = sourceType.Id;
            vacancy.ScheduleId = (await GetOrCreateScheduleAsync(headHunterVacancy))?.Id;
            vacancy.EmploymentId = (await GetOrCreateEmploymentAsync(headHunterVacancy))?.Id;
            vacancy.IsRemote = headHunterVacancy.Schedule?.Id == "remote";
            return vacancy;
        }


        private async Task<Employer> GetOrCreateEmployerAsync(SourceType sourceType,
            HeadHunterVacancy headHunterVacancy)
        {
            var sourceEmployer =
                await _storageBroker.SelectSourceEmployerByIdAndTypeIdAsync(sourceType.Id,
                    headHunterVacancy.Employer.Id) ??
                await AddSourceEmployerAsync(sourceType, headHunterVacancy.Employer);
            return sourceEmployer.Employer;
        }

        private async Task<SourceEmployer> AddSourceEmployerAsync(SourceType sourceType,
            HeadHunterEmployer headHunterEmployer)
        {
            var employer = _storageBroker.SelectAllEmployers()
                               .FirstOrDefault(item =>
                                   item.Name.ToLower().Equals(headHunterEmployer.Name.ToLower())) ??
                           await AddEmployerAsync(headHunterEmployer);
            var sourceEmployer = new SourceEmployer
            {
                SourceId = headHunterEmployer.Id,
                EmployerId = employer.Id,
                SourceTypeId = sourceType.Id,
                Url = headHunterEmployer.Url
            };
            sourceEmployer = await _storageBroker.InsertSourceEmployerAsync(sourceEmployer);
            return sourceEmployer;
        }

        private async Task<Employer> AddEmployerAsync(HeadHunterEmployer headHunterEmployer)
        {
            var employer = new Employer
            {
                Name = headHunterEmployer.Name
            };
            return await _storageBroker.InsertEmployerAsync(employer);
        }

        private async Task<Area> GetOrCreateAreaAsync(SourceType sourceType, HeadHunterVacancy headHunterVacancy)
        {
            var sourceArea =
                await _storageBroker.SelectSourceAreaByIdAndTypeIdAsync(sourceType.Id, headHunterVacancy.Area.Id) ??
                await AddSourceAreaAsync(sourceType, headHunterVacancy.Area);
            return sourceArea.Area;
        }

        private async Task<SourceArea> AddSourceAreaAsync(SourceType sourceType, HeadHunterArea headHunterArea)
        {
            var area = _storageBroker.SelectAllAreas()
                           .FirstOrDefault(item => item.Name.ToLower().Equals(headHunterArea.Name)) ??
                       await AddAreaAsync(headHunterArea);
            var sourceArea = new SourceArea
            {
                SourceId = headHunterArea.Id,
                AreaId = area.Id,
                SourceTypeId = sourceType.Id
            };

            return await _storageBroker.InsertSourceAreaAsync(sourceArea);
        }

        private async Task<Area> AddAreaAsync(HeadHunterArea headHunterArea)
        {
            var area = new Area
            {
                Name = headHunterArea.Name
            };
            return await _storageBroker.InsertAreaAsync(area);
        }

        private async Task<SourceType> AddSourceTypeAsync()
        {
            var sourceType = new SourceType
            {
                Name = ServiceCode
            };
            return await _storageBroker.InsertSourceTypeAsync(sourceType);
        }

        private async Task SaveVacancySkillsAsync(List<VacancySkill> vacancySkills)
        {
            foreach (var vacancySkill in vacancySkills) await _storageBroker.InsertVacancySkillsAsync(vacancySkill);
        }

        private async Task<List<VacancySkill>> CreateVacancySkillsAsync(Vacancy vacancy,
            List<HeadHunterSkill> keySkills)
        {
            if (keySkills == null || keySkills.Count == 0) return new List<VacancySkill>();

            var vacancySkills = new List<VacancySkill>();
            foreach (var headHunterSkill in keySkills)
            {
                if (string.IsNullOrEmpty(headHunterSkill.Name)) continue;
                var skill = await GetOrCreateSkillAsync(headHunterSkill.Name);
                var vacancySkill = new VacancySkill {VacancyId = vacancy.Id, SkillId = skill.Id};
                vacancySkills.Add(vacancySkill);
            }

            return vacancySkills;
        }

        private async Task<Skill> GetOrCreateSkillAsync(string skillName)
        {
            var trimName = skillName.Trim();
            return _storageBroker.SelectAllSkills()
                       .FirstOrDefault(skill => skill.Name.ToLower().Equals(trimName.ToLower())) ??
                   await AddSkillAsync(trimName);
        }

        private async Task<Skill> AddSkillAsync(string skillName)
        {
            var skill = new Skill {Name = skillName};
            return await _storageBroker.InsertSkillAsync(skill);
        }

        private async Task<Currency> GetOrCreateCurrencyAsync(string currencyName)
        {
            var currency = _storageBroker.SelectAllCurrencies()
                               .FirstOrDefault(currency => currency.Name.ToLower().Equals(currencyName.ToLower())) ??
                           await AddCurrencyAsync(currencyName);
            return currency;
        }

        private async Task<Currency> AddCurrencyAsync(string currencyName)
        {
            var currency = new Currency {Name = currencyName};
            return await _storageBroker.InsertCurrencyAsync(currency);
        }

        public async Task<Schedule> GetOrCreateScheduleAsync(HeadHunterVacancy headHunterVacancy)
        {
            if (headHunterVacancy.Schedule == null) return null;
            return _storageBroker.SelectAllSchedules().FirstOrDefault(s =>
                       s.Code.ToLower().Equals(headHunterVacancy.Schedule.Id.ToLower())) ??
                   await AddScheduleAsync(headHunterVacancy.Schedule);
        }

        private async Task<Schedule> AddScheduleAsync(HeadHunterSchedule headHunterSchedule)
        {
            var schedule = new Schedule {Code = headHunterSchedule.Id.ToLower(), Name = headHunterSchedule.Name};
            return await _storageBroker.InsertScheduleAsync(schedule);
        }

        private async Task<Employment> GetOrCreateEmploymentAsync(HeadHunterVacancy headHunterVacancy)
        {
            if (headHunterVacancy.Employment == null) return null;
            return _storageBroker.SelectAllEmployments().FirstOrDefault(e =>
                       e.Code.ToLower().Equals(headHunterVacancy.Employment.Id.ToLower())) ??
                   await AddEmploymentAsync(headHunterVacancy.Employment);
        }

        private async Task<Employment> AddEmploymentAsync(HeadHunterEmployment headHunterEmployment)
        {
            var employment = new Employment
                {Code = headHunterEmployment.Id.ToLower(), Name = headHunterEmployment.Name};
            return await _storageBroker.InsertEmploymentAsync(employment);
        }

        private static Vacancy CreateVacancy(HeadHunterSnippet snippet, HeadHunterVacancy headHunterVacancy)
        {
            var vacancy = new Vacancy
            {
                SourceId = headHunterVacancy.Id,
                Title = headHunterVacancy.Name,
                Descriptions = headHunterVacancy.Description,
                Responsibilities = snippet.Responsibility,
                SalaryFrom = headHunterVacancy.Salary?.From,
                SalaryTo = headHunterVacancy.Salary?.To,
                IsSalaryGross = headHunterVacancy.Salary?.IsGross,
                Url = headHunterVacancy.Url,
                RawData = headHunterVacancy.RawData,
                IsArchived = snippet.Archived,
                SourceCreatedDate = headHunterVacancy.CreatedDate,
                SourceUpdatedDate = headHunterVacancy.UpdateDate,
                ContentUpdatedDate = headHunterVacancy.UpdateDate
            };
            return vacancy;
        }
    }
}