using System.Collections.Generic;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces
{
    public interface ISkillService
    {
        Task<IList<Skill>> GetAllSkills(GetAllSkillsFilter filter = null);
    }
}