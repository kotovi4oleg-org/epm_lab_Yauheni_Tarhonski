using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class CurrencyRatesService : BaseService<CurrencyRates>, ICurrencyRatesService
    {
        private readonly IUpdateCurrencyRatesService _updateCurrencyRatesService;
        public CurrencyRatesService(DefaultContext context, IUpdateCurrencyRatesService updateCurrencyRatesService) : base(context)
        {
            _updateCurrencyRatesService = updateCurrencyRatesService;
        }
        public override async Task<CurrencyRates> GetAsync(long? id, bool tracking)
        {
            CurrencyRates resultObject;
            if (id == null) return null;
            if (tracking) 
                resultObject = await _context.CurrencyRates
                                             .Include(c => c.BaseCurrency)
                                             .Include(c => c.Currency)
                                             .SingleOrDefaultAsync(m => m.Id == id);
            else
                resultObject = await _context.CurrencyRates
                                             .Include(c => c.BaseCurrency)
                                             .Include(c => c.Currency)
                                             .AsNoTracking()
                                             .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public override IQueryable<CurrencyRates> GetIQueryable()
        {
            return _context.CurrencyRates.Include(c => c.BaseCurrency)
                                         .Include(c => c.Currency)
                                         .OrderByDescending(x => x.DateRate)
                                         .ThenBy(x => x.Currency.Name);
        }
        public SelectList GetCurrenciesIds()
        {
            return new SelectList(_context.Currency.AsNoTracking(), "Id", "Name");
        }
    }
}
