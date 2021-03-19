using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JobsWatcher.Core.Entities.Base;

namespace JobsWatcher.Core.Entities
{
    public class Skill : BaseEntity
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public List<VacancySkill> VacancySkills { get; set; }
    }
}