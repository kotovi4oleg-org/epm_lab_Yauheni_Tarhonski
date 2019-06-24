using System.Linq;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Interfaces
{
    public interface ICurrencyRatesService : IBaseService<CurrencyRates>
    {
        IQueryable<Ids> GetCurrenciesIds();
    }
}
