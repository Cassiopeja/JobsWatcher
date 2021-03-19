using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;

namespace JobsWatcher.Core.Entities
{
    public class Schedule : BaseEntity
    {
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}