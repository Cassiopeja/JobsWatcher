using System;
using System.Collections.Generic;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class VacancyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public SourceTypeDto SourceType { get; set; }
        public EmployerDto Employer { get; set; }
        public AreaDto Area { get; set; }
        public EmploymentDto Employment { get; set; }
        public ScheduleDto Schedule { get; set; }
        public bool IsRemote { get; set; }
        public string Responsibilities { get; set; }
        public string Currency { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public bool? IsSalaryGross { get; set; }
        public string Url { get; set; }
        public string Descriptions { get; set; }
        public bool IsArchived { get; set; }
        public List<VacancySkillDto> VacancySkills { get; set; }
        public DateTimeOffset SourceCreatedDate { get; set; }
        public DateTimeOffset SourceUpdatedDate { get; set; }
        public DateTimeOffset ContentUpdatedDate { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}