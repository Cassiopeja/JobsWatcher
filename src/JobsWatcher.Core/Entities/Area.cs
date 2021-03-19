using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;

namespace JobsWatcher.Core.Entities
{
    public class Area : BaseEntity
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}