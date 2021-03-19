using System.ComponentModel.DataAnnotations;

namespace JobsWatcher.Core.Entities.Source
{
    public class SourceArea : SourceEntity
    {
        [Required]
        public int AreaId { get; set; }

        [Required]
        public Area Area { get; set; }

        [Required]
        public string SourceId { get; set; }
    }
}