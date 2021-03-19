using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;

namespace JobsWatcher.Core.Entities
{
    public class SubscriptionVacancy : BaseEntity
    {
        [Required]
        public int SourceSubscriptionId { get; set; }

        [Required]
        public int VacancyId { get; set; }

        public Vacancy Vacancy { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        [Required]
        public bool IsHidden { get; set; }

        [MaxLength(5120)]
        public string Comment { get; set; }
    }
}