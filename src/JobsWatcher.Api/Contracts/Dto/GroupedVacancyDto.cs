using System.Collections.Generic;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class GroupedVacancyDto
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string HashCode {get; set; }
        public List<VacancySkillDto> VacancySkills { get; set; } = new List<VacancySkillDto>();
        public List<SimilarVacancyDto> SimilarVacancies { get; set; } = new List<SimilarVacancyDto>();
        public string Comment { get; set; }
    }
}