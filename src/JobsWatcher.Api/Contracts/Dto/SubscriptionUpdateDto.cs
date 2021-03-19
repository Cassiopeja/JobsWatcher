using System;
using System.ComponentModel.DataAnnotations;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class SubscriptionUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a SourceTypeId")]
        public int SourceTypeId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public string Parameters { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}