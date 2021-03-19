using System;
using System.Collections.Generic;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class VacancySnippetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public SourceTypeDto SourceType { get; set; }
        public EmployerDto Employer { get; set; }
        public AreaDto Area { get; set; }
        public EmploymentDto Employment { get; set; }
        public ScheduleDto Schedule { get; set; }
        public string Responsibilities { get; set; }
        public string Currency { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public List<VacancySkillDto> VacancySkills { get; set; }
        public DateTimeOffset SourceCreatedDate { get; set; }
        public DateTimeOffset ContentUpdatedDate { get; set; }
    }
}