using Newtonsoft.Json;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities
{
    public class HeadHunterSnippets
    {
        public object[] Items { get; set; }
        public int Found { get; set; }
        public int Pages { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        public int Page { get; set; }
        public object Clusters { get; set; }
        public object Arguments { get; set; }

        [JsonProperty("alternate_url")]
        public string AlternateUrl { get; set; }
    }
}