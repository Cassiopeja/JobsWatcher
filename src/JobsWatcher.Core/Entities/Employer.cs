using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;

namespace JobsWatcher.Core.Entities
{
    public class Employer : BaseEntity
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}