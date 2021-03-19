namespace JobsWatcher.Core.Entities
{
    public class GetAllSubscriptionVacanciesFilter
    {
        public string SearchText { get; set; }
        public int[] AreaId { get; set; }
        public int[] SkillId { get; set; }
        public int[] EmploymentId { get; set; }
        public int[] EmployerId { get; set; }
        public int[] ScheduleId { get; set; }
        public int[] Rating { get; set; }
        public bool? IsHidden { get; set; }
        public bool? IsArchived { get; set; }
        
    }
}