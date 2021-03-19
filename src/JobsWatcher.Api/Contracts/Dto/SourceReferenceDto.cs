using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class SourceReferenceDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public bool IsActual { get; set; }

        [Required]
        public int SourceTypeId { get; set; }

        [JsonIgnore]
        public string Parameters { get; set; }

        public JArray Data
        {
            get
            {
                if (string.IsNullOrEmpty(Parameters)) return null;
                var obj = JArray.Parse(Parameters);
                return obj;
            }
        }
    }
}