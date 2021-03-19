using System;
using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using JobsWatcher.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        public DbSet<Vacancy> Vacancies { get; set; }

        public IQueryable<Vacancy> SelectAllVacancies()
        {
            return Vacancies.AsQueryable();
        }

        public async Task<Vacancy> SelectVacancyByIdAsync(int vacancyId)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await Vacancies
                .IncludeAllProperties()
                .FirstOrDefaultAsync(vacancy => vacancy.Id == vacancyId);
        }

        public async Task<Vacancy> SelectVacancyBySourceIdAsync(int sourceTypeId, string sourceId)
        {
            return await Vacancies
                .IncludeAllProperties()
                .FirstOrDefaultAsync(vacancy =>
                    vacancy.SourceId == sourceId && vacancy.SourceTypeId == sourceTypeId);
        }

        public async Task<Vacancy> InsertVacancyAsync(Vacancy vacancy)
        {
            vacancy.CreatedDate = DateTimeOffset.Now;
            vacancy.UpdatedDate = DateTimeOffset.Now;
            vacancy.HashCode = _hashService.GetHashString(vacancy.Descriptions);
            var vacancyEntry = await Vacancies.AddAsync(vacancy);
            await SaveChangesAsync();
            return vacancyEntry.Entity;
        }

        public async Task<Vacancy> UpdateVacancyAsync(Vacancy vacancy)
        {
            vacancy.UpdatedDate = DateTimeOffset.Now;
            vacancy.HashCode = _hashService.GetHashString(vacancy.Descriptions);
            var local = Vacancies
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(vacancy.Id));

            // check if local is not null 
            if (local != null)
                // detach
                Entry(local).State = EntityState.Detached;
            // set Modified flag in your entry
            // var skills = vacancy.Skills;
            // vacancy.Skills.Clear();
            Entry(vacancy).State = EntityState.Modified;
            var vacancyEntry = Vacancies.Update(vacancy);
            await SaveChangesAsync();
            return vacancyEntry.Entity;
        }

        public async Task<Vacancy> DeleteVacancyAsync(Vacancy vacancy)
        {
            var vacancyEntry = Vacancies.Remove(vacancy);
            await SaveChangesAsync();
            return vacancyEntry.Entity;
        }
    }
}