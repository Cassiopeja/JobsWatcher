using System.ComponentModel.DataAnnotations;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class SubscriptionVacancyDto
    {
        [Required]
        public int Id { get; set; }
        public VacancyDto Vacancy { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        public bool IsHidden { get; set; }
        public string Comment { get; set; }
        public int SourceSubscriptionId { get; set; }
    }
}