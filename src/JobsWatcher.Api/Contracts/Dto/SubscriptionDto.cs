using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class SubscriptionDto
    {
        public int Id { get; set; }

        [Required]
        public SourceTypeDto SourceType { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [JsonIgnore]
        public string Parameters { get; set; }

        public JObject Data
        {
            get
            {
                if (string.IsNullOrEmpty(Parameters)) return null;
                var obj = JObject.Parse(Parameters);
                return obj;
            }
        }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
    }
}