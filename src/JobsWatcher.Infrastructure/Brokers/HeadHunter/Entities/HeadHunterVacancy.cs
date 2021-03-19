using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JobsWatcher.Infrastructure.Brokers.HeadHunter.Entities
{
    public class HeadHunterVacancy
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("premium")]
        public bool IsPremium { get; set; }

        public HeadHunterSalary Salary { get; set; }
        public HeadHunterArea Area { get; set; }
        public string Description { get; set; }
        public HeadHunterSchedule Schedule { get; set; }

        [JsonProperty("key_skills")]
        public List<HeadHunterSkill> KeySkills { get; set; }

        [JsonProperty("alternate_url")]
        public string Url { get; set; }

        [JsonProperty("archived")]
        public bool IsArchived { get; set; }

        public HeadHunterEmployer Employer { get; set; }

        public HeadHunterEmployment Employment { get; set; }
        public string RawData { get; set; }

        [JsonProperty("published_at")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset UpdateDate { get; set; }
    }
}