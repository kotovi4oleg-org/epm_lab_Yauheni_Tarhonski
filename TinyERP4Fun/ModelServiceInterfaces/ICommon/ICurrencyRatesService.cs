using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface ICurrencyRatesService : IBaseService<CurrencyRates>
    {
        Task UpdateBYNVoid();
        SelectList GetCurrenciesIds();
    }
}
