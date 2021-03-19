using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter
{
    public interface IHeadHunterBroker
    {
        IAsyncEnumerable<HeadHunterSnippet> GetSnippets(HeadHunterSubscriptionParameters subscriptionParameters);

        Task<HeadHunterVacancy> GetVacancy(string id);
    }
}