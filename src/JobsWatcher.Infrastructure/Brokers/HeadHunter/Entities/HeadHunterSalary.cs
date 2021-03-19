namespace JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities
{
    public class HeadHunterSalary
    {
        public int? From { get; set; }
        public int? To { get; set; }
        public string Currency { get; set; }
        public bool? IsGross { get; set; }
    }
}