using Microsoft.AspNetCore.Mvc;

namespace JobsWatcher.Api.Contracts.Requests.Queries
{
    public class GetAllSkillsQuery
    {
        [FromQuery]
        public string SearchText { get; set; }

        [FromQuery]
        public int? SubscriptionId { get; set; }
        
        [FromQuery]
        public int? Limit { get; set; }
    }
}