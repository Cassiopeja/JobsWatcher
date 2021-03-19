using Newtonsoft.Json;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities
{
    public class HeadHunterEmployer
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("alternate_url")]
        public string Url { get; set; }
    }
}