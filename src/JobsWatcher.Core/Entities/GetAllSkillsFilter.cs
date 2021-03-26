namespace JobsWatcher.Core.Entities
{
    public class GetAllSkillsFilter
    {
        public string SearchText { get; set; }
        public int? SubscriptionId { get; set; }
        public int? Limit { get; set; }
    }
}