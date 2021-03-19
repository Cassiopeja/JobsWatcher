using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities;
using JobsWatcher.Infrastructure.UnitTests.Extensions;
using Moq;
using Xunit;

namespace JobsWatcher.Infrastructure.UnitTests.Services.HeadHunter
{
    public partial class HeadHunterServiceTests
    {

        [Fact]
        public async Task ShouldAddNewVacancyIfNotExistedInDb()
        {
            //arrange
            var snippetsResponse = new List<HeadHunterSnippet> {GetRandomHeadHunterSnippet()};
            var vacancyResponse = GetRandomHeadHunterVacancy();
            SourceEmployer nullSourceEmployer = null;
            SourceArea nullSourceArea = null;
            Vacancy nullVacancy = null;
            var returnedSourceType = _headHunterSourceType;
            var returnedEmployer = new Employer {Name = vacancyResponse.Employer.Name, Id = 1};
            var returnedSourceEmployer = GetSourceEmployer(returnedEmployer, vacancyResponse, returnedSourceType);
            var returnedArea = new Area {Id = 1, Name = vacancyResponse.Area.Name};
            var returnedSourceArea = GetSourceArea(returnedArea, vacancyResponse, returnedSourceType);
            var returnedCurrency = GetCurrency(vacancyResponse); 
            var returnedVacancy = GetRandomVacancy();
            var returnedSkill = new Skill {Id = 1, Name = "C#"};
            var returnedSubscriptions = new[] {GetRandomSourceSubscription()}.AsAsyncQueryable();

            var emptyEmployers = new List<Employer>().AsQueryable();
            var emptyAreas = new List<Area>().AsQueryable();
            var emptyCurrencies = new List<Currency>().AsQueryable();
            var emptySkills = new List<Skill>().AsQueryable();
            var emptyEmployments = new List<Employment>().AsQueryable();
            var emptySchedules = new List<Schedule>().AsQueryable();

            var actualSourceTypeName = "";
            var expectedSourceTypeName = returnedSourceType.Name;

            _headHunterBrokerMock.Setup(broker => broker.GetSnippets(It.IsAny<HeadHunterSubscriptionParameters>()))
                .Returns(GetHeadHunterSnippetsAsync(snippetsResponse));
            _headHunterBrokerMock.Setup(broker => broker.GetVacancy(It.IsAny<string>())).ReturnsAsync(vacancyResponse);
            _storageBrokerMock.Setup(broker => broker.SelectAllSourceSubscriptions()).Returns(returnedSubscriptions);
            _storageBrokerMock.Setup(broker => broker.InsertSourceTypeAsync(It.IsAny<SourceType>()))
                .Callback<SourceType>(param => actualSourceTypeName = param.Name)
                .ReturnsAsync(returnedSourceType);
            _storageBrokerMock.Setup(broker => broker.SelectVacancyBySourceIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(nullVacancy);
            _storageBrokerMock.Setup(broker =>
                    broker.SelectSourceEmployerByIdAndTypeIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(nullSourceEmployer);
            _storageBrokerMock.Setup(broker => broker.SelectAllEmployers()).Returns(emptyEmployers);
            _storageBrokerMock.Setup(broker => broker.InsertEmployerAsync(It.IsAny<Employer>()))
                .ReturnsAsync(returnedEmployer);
            _storageBrokerMock.Setup(broker => broker.InsertSourceEmployerAsync(It.IsAny<SourceEmployer>()))
                .ReturnsAsync(returnedSourceEmployer);
            _storageBrokerMock.Setup(broker =>
                    broker.SelectSourceAreaByIdAndTypeIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(nullSourceArea);
            _storageBrokerMock.Setup(broker => broker.SelectAllAreas()).Returns(emptyAreas);
            _storageBrokerMock.Setup(broker => broker.InsertAreaAsync(It.IsAny<Area>())).ReturnsAsync(returnedArea);
            _storageBrokerMock.Setup(broker => broker.InsertSourceAreaAsync(It.IsAny<SourceArea>()))
                .ReturnsAsync(returnedSourceArea);
            _storageBrokerMock.Setup(broker => broker.SelectAllCurrencies()).Returns(emptyCurrencies);
            _storageBrokerMock.Setup(broker => broker.InsertCurrencyAsync(It.IsAny<Currency>()))
                .ReturnsAsync(returnedCurrency);
            _storageBrokerMock.Setup(broker => broker.InsertVacancyAsync(It.IsAny<Vacancy>()))
                .ReturnsAsync(returnedVacancy);
            _storageBrokerMock.Setup(broker => broker.SelectAllSkills()).Returns(emptySkills);
            _storageBrokerMock.Setup(broker => broker.InsertSkillAsync(It.IsAny<Skill>())).ReturnsAsync(returnedSkill);
            _storageBrokerMock.Setup(broker => broker.SelectAllSchedules()).Returns(emptySchedules);
            _storageBrokerMock.Setup(broker => broker.SelectAllEmployments()).Returns(emptyEmployments);

            //act
            await _sourceService.UpdateAllSubscriptions();

            //assert
            _headHunterBrokerMock.Verify(broker => broker.GetSnippets(It.IsAny<HeadHunterSubscriptionParameters>()),
                Times.Once);
            _headHunterBrokerMock.Verify(broker => broker.GetVacancy(It.IsAny<string>()), Times.Once);

            _storageBrokerMock.Verify(broker => broker.SelectAllSourceSubscriptions(), Times.Once);
            actualSourceTypeName.Should().Equals(expectedSourceTypeName);
            _storageBrokerMock.Verify(
                broker => broker.SelectVacancyBySourceIdAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _storageBrokerMock.Verify(
                broker => broker.SelectSourceEmployerByIdAndTypeIdAsync(It.IsAny<int>(), It.IsAny<string>()),
                Times.Once);
            _storageBrokerMock.Verify(broker => broker.SelectAllEmployers(), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertEmployerAsync(It.IsAny<Employer>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertSourceEmployerAsync(It.IsAny<SourceEmployer>()),
                Times.Once);
            _storageBrokerMock.Verify(
                broker => broker.SelectSourceAreaByIdAndTypeIdAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.SelectAllAreas(), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertAreaAsync(It.IsAny<Area>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertSourceAreaAsync(It.IsAny<SourceArea>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.SelectAllCurrencies(), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertCurrencyAsync(It.IsAny<Currency>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertVacancyAsync(It.IsAny<Vacancy>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.SelectAllSkills(), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertSkillAsync(It.IsAny<Skill>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.SelectAllSchedules(), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertScheduleAsync(It.IsAny<Schedule>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.SelectAllEmployments(), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertEmploymentAsync(It.IsAny<Employment>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertVacancySkillsAsync(It.IsAny<VacancySkill>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertSubscriptionVacancyAsync(It.IsAny<SubscriptionVacancy>()),
                Times.Once);

            _storageBrokerMock.VerifyNoOtherCalls();
            _headHunterBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldUpdateVacancyWhenHeadHunterVacancyUpdatedDateIsNewer()
        {
            // arrange
            var snippetResponse = GetRandomHeadHunterSnippet();
            var snippetsResponse = new List<HeadHunterSnippet> {snippetResponse};
            var headHunterVacancyResponse = GetRandomHeadHunterVacancy();
            var sourceType = _headHunterSourceType;
            var returnedVacancy = GetOlderVersionSameVacancy(headHunterVacancyResponse, snippetResponse);
            var returnedSourceEmployer = _sourceEmployerFaker.Generate();
            var returnedSourceArea = _sourceAreaFaker.Generate();
            var returnedCurrency = GetCurrency(headHunterVacancyResponse);
            var returnedSchedule = GetSchedule(headHunterVacancyResponse);
            var returnedEmployment = GetEmployment(headHunterVacancyResponse);
            var returnedSubscriptions = new[] {GetRandomSourceSubscription()}.AsAsyncQueryable();
            SubscriptionVacancy nullSubscriptionVacancy = null;

            _headHunterBrokerMock.Setup(broker => broker.GetSnippets(It.IsAny<HeadHunterSubscriptionParameters>()))
                .Returns(GetHeadHunterSnippetsAsync(snippetsResponse));
            _headHunterBrokerMock.Setup(broker => broker.GetVacancy(It.IsAny<string>()))
                .ReturnsAsync(headHunterVacancyResponse);
            _storageBrokerMock.Setup(broker => broker.SelectAllSourceSubscriptions()).Returns(returnedSubscriptions);
            _storageBrokerMock.Setup(broker => broker.SelectVacancyBySourceIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(returnedVacancy);
            _storageBrokerMock.Setup(broker =>
                    broker.SelectSourceEmployerByIdAndTypeIdAsync(sourceType.Id, It.IsAny<string>()))
                .ReturnsAsync(returnedSourceEmployer);
            _storageBrokerMock.Setup(broker =>
                    broker.SelectSourceAreaByIdAndTypeIdAsync(sourceType.Id, It.IsAny<string>()))
                .ReturnsAsync(returnedSourceArea);
            _storageBrokerMock.Setup(broker =>
                broker.SelectAllCurrencies()).Returns(new List<Currency> {returnedCurrency}.AsQueryable);
            _storageBrokerMock.Setup(broker =>
                broker.SelectAllSchedules()).Returns(new List<Schedule> {returnedSchedule}.AsQueryable);
            _storageBrokerMock.Setup(broker =>
                broker.SelectAllEmployments()).Returns(new List<Employment> {returnedEmployment}.AsQueryable);
            _storageBrokerMock
                .Setup(broker =>
                    broker.SelectSubscriptionVacancyByVacancyAndSubscriptionIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(nullSubscriptionVacancy);

            // act
            await _sourceService.UpdateAllSubscriptions();

            // assert
            _headHunterBrokerMock.Verify(broker => broker.GetSnippets(It.IsAny<HeadHunterSubscriptionParameters>()),
                Times.Once);
            _headHunterBrokerMock.Verify(broker => broker.GetVacancy(It.IsAny<string>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.SelectAllSourceSubscriptions(), Times.Once);
            _storageBrokerMock.Verify(
                broker => broker.SelectVacancyBySourceIdAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _storageBrokerMock.Verify(
                broker => broker.SelectSourceEmployerByIdAndTypeIdAsync(It.IsAny<int>(), It.IsAny<string>()),
                Times.Once());
            _storageBrokerMock.Verify(
                broker => broker.SelectSourceAreaByIdAndTypeIdAsync(It.IsAny<int>(), It.IsAny<string>()),
                Times.Once());
            _storageBrokerMock.Verify(
                broker => broker.SelectAllCurrencies(), Times.Once());
            _storageBrokerMock.Verify(
                broker => broker.SelectAllSchedules(), Times.Once());
            _storageBrokerMock.Verify(
                broker => broker.SelectAllEmployments(), Times.Once());
            _storageBrokerMock.Verify(
                broker => broker.UpdateVacancyAsync(It.IsAny<Vacancy>()));
            _storageBrokerMock.Verify(broker => broker.InsertSubscriptionVacancyAsync(It.IsAny<SubscriptionVacancy>()),
                Times.Once);
            _storageBrokerMock.Verify(
                broker => broker.SelectSubscriptionVacancyByVacancyAndSubscriptionIdAsync(It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Once);
            _storageBrokerMock.VerifyNoOtherCalls();
            _headHunterBrokerMock.VerifyNoOtherCalls();
        }


        [Fact]
        public async Task ShouldNotUpdateVacancyWhenHeadHunterVacancyUpdatedDateIsOlder()
        {
            // arrange
            var snippetResponse = GetRandomHeadHunterSnippet();
            snippetResponse.Archived = false;
            var snippetsResponse = new List<HeadHunterSnippet> {snippetResponse};
            var headHunterVacancyResponse = GetRandomHeadHunterVacancy();
            var returnedVacancy = GetVacancy(headHunterVacancyResponse, snippetResponse);
            var returnedSubscriptions = new[] {GetRandomSourceSubscription()}.AsAsyncQueryable();
            SubscriptionVacancy nullSubscriptionVacancy = null;

            _headHunterBrokerMock.Setup(broker => broker.GetSnippets(It.IsAny<HeadHunterSubscriptionParameters>()))
                .Returns(GetHeadHunterSnippetsAsync(snippetsResponse));
            _storageBrokerMock.Setup(broker => broker.SelectAllSourceSubscriptions()).Returns(returnedSubscriptions);
            _storageBrokerMock.Setup(broker => broker.SelectVacancyBySourceIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(returnedVacancy);
            _storageBrokerMock
                .Setup(broker =>
                    broker.SelectSubscriptionVacancyByVacancyAndSubscriptionIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(nullSubscriptionVacancy);

            // act
            await _sourceService.UpdateAllSubscriptions();

            // assert
            _headHunterBrokerMock.Verify(broker => broker.GetSnippets(It.IsAny<HeadHunterSubscriptionParameters>()),
                Times.Once);
            _storageBrokerMock.Verify(broker => broker.SelectAllSourceSubscriptions(), Times.Once);
            _storageBrokerMock.Verify(
                broker => broker.SelectVacancyBySourceIdAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _storageBrokerMock.Verify(broker => broker.InsertSubscriptionVacancyAsync(It.IsAny<SubscriptionVacancy>()),
                Times.Once);
            _storageBrokerMock.Verify(
                broker => broker.SelectSubscriptionVacancyByVacancyAndSubscriptionIdAsync(It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Once);

            _storageBrokerMock.VerifyNoOtherCalls();
            _headHunterBrokerMock.VerifyNoOtherCalls();
        }
    }
}