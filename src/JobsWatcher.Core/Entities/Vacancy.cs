using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;
using JobsWatcher.Core.Entities.Source;

namespace JobsWatcher.Core.Entities
{
    public class Vacancy : BaseEntity
    {
        [Required]
        public int SourceTypeId { get; set; }

        [Required]
        public SourceType SourceType { get; set; }

        [Required]
        public int EmployerId { get; set; }

        [Required]
        public Employer Employer { get; set; }

        public int? AreaId { get; set; }
        public Area Area { get; set; }
        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public List<VacancySkill> VacancySkills { get; set; }

        [Required]
        public string SourceId { get; set; }

        public int? EmploymentId { get; set; }
        public Employment Employment { get; set; }
        public int? ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public bool IsRemote { get; set; }

        [Required]
        [MaxLength(512)]
        public string Title { get; set; }

        public string Responsibilities { get; set; }
        public string Descriptions { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public bool? IsSalaryGross { get; set; }
        public string Url { get; set; }

        [Required]
        public bool IsArchived { get; set; }

        public string RawData { get; set; }

        [MaxLength(64)]
        public string HashCode { get; set; }

        public DateTimeOffset SourceCreatedDate { get; set; }
        public DateTimeOffset SourceUpdatedDate { get; set; }
        public DateTimeOffset ContentUpdatedDate { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public bool HasContentChanges(Vacancy updatedVacancy)
        {
            return Title != updatedVacancy.Title || Descriptions != updatedVacancy.Descriptions
                                                 || Responsibilities != updatedVacancy.Responsibilities
                                                 || SalaryFrom != updatedVacancy.SalaryFrom
                                                 || SalaryTo != updatedVacancy.SalaryTo;
        }
    }
}