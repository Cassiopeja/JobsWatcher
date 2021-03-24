namespace JobsWatcher.Core.Entities
{
    public class GetAllSubscriptionVacanciesFilter
    {
        public string SearchText { get; set; }
        public int[] AreaIds { get; set; }
        public int[] SkillIds { get; set; }
        public int[] EmploymentIds { get; set; }
        public int[] EmployerIds { get; set; }
        public int[] ScheduleIds { get; set; }
        public int[] Ratings { get; set; }
        public bool? IsHidden { get; set; }
        public bool? IsArchived { get; set; }
        
    }
}