using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsWatcher.Core.Entities.Source
{
    public class SourceSubscription : SourceEntity
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Column(TypeName = "jsonb")]
        public string Parameters { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        public SourceSubscription DeepClone()
        {
            var clone = (SourceSubscription) MemberwiseClone();
            if (SourceType != null) clone.SourceType = SourceType.DeepClone();
            return clone;
        }
    }
}