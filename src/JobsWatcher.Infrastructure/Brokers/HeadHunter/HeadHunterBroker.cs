using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities;
using JobsWatcher.Infrastructure.Brokers.HeadHunter.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter
{
    public class HeadHunterBroker : IHeadHunterBroker
    {
        private readonly IHeadHunterApi _headHunterApi;
        private readonly ILogger<HeadHunterBroker> _logger;
        private readonly int _timeOut = 100;

        public HeadHunterBroker(IHeadHunterApi headHunterApi, ILogger<HeadHunterBroker> logger)
        {
            _headHunterApi = headHunterApi;
            _logger = logger;
        }

        public async IAsyncEnumerable<HeadHunterSnippet> GetSnippets(
            HeadHunterSubscriptionParameters subscriptionParameters)
        {
            //var queryParameters = GetQueryParameters(subscriptionParameters, DateTime.Now.AddDays(-7), DateTime.Now);
            var queryParameters = GetQueryParameters(subscriptionParameters);
            var pagesCount = 1;
            var currentPage = 0;
            do
            {
                var snippets = await _headHunterApi.GetVacanciesSnippets(queryParameters);
                if (currentPage == 0)
                {
                    _logger.LogInformation("Total number vacancies {totalVacanciesCount}, pages {totalPagesCount}",
                        snippets.Found, snippets.Pages);
                    pagesCount = snippets.Pages;
                }

                _logger.LogInformation("Processing page {CurrentPageNumber}", currentPage + 1);

                foreach (var data in snippets.Items)
                {
                    if (data == null) continue;
                    var snippet = JObject.Parse(data.ToString() ?? string.Empty);
                    var vacancy = new HeadHunterSnippet
                    {
                        Id = (string) snippet["id"],
                        Name = (string) snippet["name"],
                        CreatedDate = (DateTimeOffset) snippet["created_at"],
                        UpdateDate = (DateTimeOffset) snippet["published_at"],
                        Responsibility = (string) snippet["snippet"]?["responsibility"]
                    };

                    yield return vacancy;
                    await Task.Delay(_timeOut);
                }

                currentPage++;
                queryParameters.PageNumber++;
            } while (currentPage < pagesCount);
        }

        public async Task<HeadHunterVacancy> GetVacancy(string id)
        {
            try
            {
                var response = await _headHunterApi.GetVacancy(id);
                var vacancy = JsonConvert.DeserializeObject<HeadHunterVacancy>(response);
                vacancy.RawData = response;
                return vacancy;
            }
            catch (ApiException exc)
                when (exc.StatusCode == HttpStatusCode.NotFound)
            {
                throw new VacancyNotFound(id);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, "");
                throw;
            }
        }

        private HeadHunterQueryParameters GetQueryParameters(HeadHunterSubscriptionParameters subscriptionParameters,
            DateTime? dateFrom = null,
            DateTime? dateTo = null)
        {
            return new()
            {
                SpecializationId = subscriptionParameters.SpecializationId,
                SearchText = subscriptionParameters.SearchText,
                Schedules = subscriptionParameters.Schedules,
                Areas = subscriptionParameters.Areas,
                PageNumber = 0,
                ItemsPerPage = 100,
                DateFrom = dateFrom == null? null: $"{dateFrom:yyyy-MM-dd}",
                DateTo = dateTo == null?null:$"{dateTo:yyyy-MM-dd}"
            };
        }
    }
}