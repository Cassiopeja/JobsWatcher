using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities;
using Refit;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter
{
    public interface IHeadHunterApi
    {
        [Get("/vacancies")]
        Task<HeadHunterSnippets> GetVacanciesSnippets(HeadHunterQueryParameters parameters);

        [Get("/vacancies")]
        Task<HeadHunterSnippets> GetVacanciesSnippets(string specialization,
            [Query(CollectionFormat.Multi)] IEnumerable<string> schedule,
            string text,
            [AliasAs("per_page")] int perPage,
            int page);

        [Get("/vacancies/{id}")]
        Task<string> GetVacancy(string id);
    }
}