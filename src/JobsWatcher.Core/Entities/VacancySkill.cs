using System.ComponentModel.DataAnnotations;

namespace JobsWatcher.Core.Entities
{
    public class VacancySkill
    {
        [Required]
        public int VacancyId { get; set; }

        [Required]
        public Vacancy Vacancy { get; set; }

        [Required]
        public int SkillId { get; set; }

        [Required]
        public Skill Skill { get; set; }
    }
}