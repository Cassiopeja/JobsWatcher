using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;

namespace JobsWatcher.Core.Entities.Source
{
    public abstract class SourceEntity : BaseEntity
    {
        [Required]
        public int SourceTypeId { get; set; }

        [Required]
        public SourceType SourceType { get; set; }
    }
}