using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<VacancySkill> selectAllVacancySkills();
        Task<VacancySkill> SelectVacancySkillsByIdAsync(int skillId, int vacancyId);
        Task<ICollection<VacancySkill>> SelectSkillVacanciesByVacancyIdAsync(int vacancyId);
        Task<VacancySkill> InsertVacancySkillsAsync(VacancySkill vacancySkill);
        Task<VacancySkill> DeleteVacancySkillsAsync(VacancySkill vacancySkill);
    }
}