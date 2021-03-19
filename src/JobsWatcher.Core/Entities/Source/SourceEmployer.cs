using System.ComponentModel.DataAnnotations;

namespace JobsWatcher.Core.Entities.Source
{
    public class SourceEmployer : SourceEntity
    {
        [Required]
        public int EmployerId { get; set; }

        [Required]
        public Employer Employer { get; set; }

        [Required]
        public string SourceId { get; set; }

        public string Url { get; set; }
    }
}