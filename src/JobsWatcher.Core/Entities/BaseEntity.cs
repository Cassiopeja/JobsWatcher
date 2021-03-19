using System.ComponentModel.DataAnnotations;

namespace JobsWatcher.Core.Entities.Base
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}