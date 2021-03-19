using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<Skill> Skills { get; set; }

        public IQueryable<Skill> SelectAllSkills()
        {
            return Skills.AsQueryable();
        }

        public async Task<Skill> SelectSkillByIdAsync(int skillId)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await Skills.FindAsync(skillId);
        }

        public async Task<Skill> InsertSkillAsync(Skill skill)
        {
            var skillEntry = await Skills.AddAsync(skill);
            await SaveChangesAsync();
            return skillEntry.Entity;
        }

        public async Task<Skill> UpdateSkillAsync(Skill skill)
        {
            var skillEntry = Skills.Update(skill);
            await SaveChangesAsync();
            return skillEntry.Entity;
        }

        public async Task<Skill> DeleteSkillAsync(Skill skill)
        {
            var skillEntry = Skills.Remove(skill);
            await SaveChangesAsync();
            return skillEntry.Entity;
        }
    }
}