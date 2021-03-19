using System;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities
{
    public class HeadHunterSnippet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Responsibility { get; set; }
        public bool Archived { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
    }
}