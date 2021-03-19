using Microsoft.AspNetCore.Mvc;

namespace JobsWatcher.Api.Contracts.Requests.Queries
{
    public class GetAllSubscriptionVacanciesQuery
    {
        [FromQuery]
        public string SearchText { get; set; }
        [FromQuery]
        public int[] AreaId { get; set; }
        [FromQuery]
        public int[] SkillId { get; set; }
        [FromQuery]
        public int[] EmploymentId { get; set; }
        [FromQuery]
        public int[] EmployerId { get; set; }
        [FromQuery]
        public int[] ScheduleId { get; set; }
        [FromQuery]
        public int[] Rating { get; set; }
        [FromQuery]
        public bool? IsHidden { get; set; }
        [FromQuery]
        public bool? IsArchived { get; set; }
    }
}