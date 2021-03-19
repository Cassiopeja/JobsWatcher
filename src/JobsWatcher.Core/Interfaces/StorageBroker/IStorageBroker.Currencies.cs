using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;

namespace JobsWatcher.Core.Interfaces.StorageBroker
{
    public partial interface IStorageBroker
    {
        IQueryable<Currency> SelectAllCurrencies();
        Task<Currency> SelectCurrencyByIdAsync(int currencyId);
        Task<Currency> InsertCurrencyAsync(Currency currency);
        Task<Currency> UpdateCurrencyAsync(Currency currency);
        Task<Currency> DeleteCurrencyAsync(Currency currency);
    }
}