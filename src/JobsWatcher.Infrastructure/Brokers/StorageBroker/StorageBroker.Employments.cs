using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        private DbSet<Employment> Employments { get; set; }

        public IQueryable<Employment> SelectAllEmployments()
        {
            return Employments.AsQueryable();
        }

        public async Task<Employment> SelectEmploymentByIdAsync(int employmentId)
        {
            return await Employments.FindAsync(employmentId);
        }

        public async Task<Employment> InsertEmploymentAsync(Employment employment)
        {
            var employmentEntry = await Employments.AddAsync(employment);
            await SaveChangesAsync();
            return employmentEntry.Entity;
        }

        public async Task<Employment> UpdateEmploymentAsync(Employment employment)
        {
            var employmentEntry = Employments.Update(employment);
            await SaveChangesAsync();
            return employmentEntry.Entity;
        }

        public async Task<Employment> DeleteEmploymentAsync(Employment employment)
        {
            var employmentEntry = Employments.Remove(employment);
            await SaveChangesAsync();
            return employmentEntry.Entity;
        }
    }
}