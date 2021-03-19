using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker
    {
        private DbSet<Currency> Currencies { get; set; }

        public IQueryable<Currency> SelectAllCurrencies()
        {
            return Currencies.AsQueryable();
        }

        public async Task<Currency> SelectCurrencyByIdAsync(int currencyId)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await Currencies.FindAsync(currencyId);
        }

        public async Task<Currency> InsertCurrencyAsync(Currency currency)
        {
            var currencyEntry = await Currencies.AddAsync(currency);
            await SaveChangesAsync();
            return currencyEntry.Entity;
        }

        public async Task<Currency> UpdateCurrencyAsync(Currency currency)
        {
            var currencyEntry = Currencies.Update(currency);
            await SaveChangesAsync();
            return currencyEntry.Entity;
        }

        public async Task<Currency> DeleteCurrencyAsync(Currency currency)
        {
            var currencyEntry = Currencies.Remove(currency);
            await SaveChangesAsync();
            return currencyEntry.Entity;
        }
    }
}