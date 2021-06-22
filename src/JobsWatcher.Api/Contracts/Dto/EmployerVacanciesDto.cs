using System.Collections.Generic;

namespace JobsWatcher.Api.Contracts.Dto
{
    public class EmployerVacanciesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupedVacancyDto> GroupedVacancies { get; set; }
    }
}