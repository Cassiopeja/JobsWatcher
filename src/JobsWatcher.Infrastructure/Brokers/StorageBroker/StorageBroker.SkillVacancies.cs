using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        private DbSet<VacancySkill> VacancySkills { get; set; }

        public IQueryable<VacancySkill> selectAllVacancySkills()
        {
            return VacancySkills.AsQueryable();
        }

        public async Task<VacancySkill> SelectVacancySkillsByIdAsync(int skillId, int vacancyId)
        {
            return await VacancySkills.FindAsync(vacancyId, skillId);
        }

        public async Task<ICollection<VacancySkill>> SelectSkillVacanciesByVacancyIdAsync(int vacancyId)
        {
            return await VacancySkills.Where(sv => sv.VacancyId == vacancyId).ToListAsync();
        }

        public async Task<VacancySkill> InsertVacancySkillsAsync(VacancySkill vacancySkill)
        {
            var existed = await SelectVacancySkillsByIdAsync(vacancySkill.SkillId, vacancySkill.VacancyId);
            if (existed != null)
            {
                return existed;
            }
            var skillVacancyEntry = await VacancySkills.AddAsync(vacancySkill);
            await SaveChangesAsync();
            return skillVacancyEntry.Entity;
        }

        public async Task<VacancySkill> DeleteVacancySkillsAsync(VacancySkill vacancySkill)
        {
            var skillVacancyEntry = VacancySkills.Remove(vacancySkill);
            await SaveChangesAsync();
            return skillVacancyEntry.Entity;
        }
    }
}