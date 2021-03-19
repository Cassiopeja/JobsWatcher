using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<Skill> SelectAllSkills();
        Task<Skill> SelectSkillByIdAsync(int skillId);
        Task<Skill> InsertSkillAsync(Skill skill);
        Task<Skill> UpdateSkillAsync(Skill skill);
        Task<Skill> DeleteSkillAsync(Skill skill);
    }
}