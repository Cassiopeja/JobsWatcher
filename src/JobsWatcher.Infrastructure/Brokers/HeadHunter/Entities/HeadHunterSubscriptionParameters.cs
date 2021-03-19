namespace JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities
{
    public class HeadHunterSubscriptionParameters
    {
        public int SpecializationId { get; set; }

        public string[] Schedules { get; set; }
        
        public string[] Areas { get; set; }

        public string SearchText { get; set; }
    }
}