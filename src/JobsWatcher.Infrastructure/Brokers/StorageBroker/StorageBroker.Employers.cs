using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<Employer> Employers { get; set; }

        public IQueryable<Employer> SelectAllEmployers()
        {
            return Employers.AsQueryable();
        }

        public async Task<Employer> SelectEmployerByIdAsync(int employerId)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await Employers.FindAsync(employerId);
        }

        public async Task<Employer> InsertEmployerAsync(Employer employer)
        {
            var employerEntry = await Employers.AddAsync(employer);
            await SaveChangesAsync();
            return employerEntry.Entity;
        }

        public async Task<Employer> UpdateEmployerAsync(Employer employer)
        {
            var employerEntry = Employers.Update(employer);
            await SaveChangesAsync();
            return employerEntry.Entity;
        }

        public async Task<Employer> DeleteEmployerAsync(Employer employer)
        {
            var employerEntry = Employers.Remove(employer);
            await SaveChangesAsync();
            return employerEntry.Entity;
        }
    }
}