using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;

namespace JobsWatcher.Core.Entities.Source
{
    public class SourceType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public SourceType DeepClone()
        {
            return (SourceType) MemberwiseClone();
        }
    }
}