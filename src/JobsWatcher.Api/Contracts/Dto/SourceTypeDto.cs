using System.ComponentModel.DataAnnotations;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class SourceTypeDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}