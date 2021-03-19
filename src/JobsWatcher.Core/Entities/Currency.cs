using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;

namespace JobsWatcher.Core.Entities
{
    public class Currency : BaseEntity
    {
        [Required]
        [MaxLength(3)]
        public string Name { get; set; }
    }
}