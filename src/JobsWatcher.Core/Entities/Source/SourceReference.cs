using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsWatcher.Core.Entities.Source
{
    public class SourceReference : SourceEntity
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "jsonb")]
        public string Parameters { get; set; }
        [Required]
        public bool IsActual { get; set; }
    }
}