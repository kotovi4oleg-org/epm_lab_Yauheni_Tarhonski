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
        public CurrencyRatesService(DefaultContext context) : base(context)
        {
        }

        [DataContract]
        internal class NbrbCurOrg
        {
            [DataMember]
            public int Cur_ID { get; set; }
            [DataMember]
            public string Cur_Abbreviation { get; set; }
            [DataMember]
            public int Cur_Scale { get; set; }
            [DataMember]
            public string Cur_DateStart { get; set; }
            [DataMember]
            public string Cur_DateEnd { get; set; }
        }
        [DataContract]
        internal class NbrbCur
        {
            [DataMember]
            public int Cur_ID { get; set; }
            [DataMember]
            public string Cur_Abbreviation { get; set; }
            [DataMember]
            public int Cur_Scale { get; set; }
            [DataMember]
            public string Cur_DateStart { get; set; }
            [DataMember]
            public string Cur_DateEnd { get; set; }
        }
        [DataContract]
        internal class NbrbRate
        {
            [DataMember]
            public int Cur_Scale = default;
            [DataMember]
            public double Cur_OfficialRate = default;
        }
        public async Task UpdateBYNVoid()
        {
            Currency baseCurrency = await _context.Currency.Where(x => x.Code == Constants.BYNCode).SingleOrDefaultAsync();
            var currencyList = _context.Currency.Where(x => x.Code != Constants.BYNCode && x.Active);
            string jsonCurList = await ServicesCommonFunctions.SendGetRequestAsync(new Uri(Constants.BYN_CURRENCY_LIST_URL));
            DataContractJsonSerializer jsonFormatterNBRBCurAr = new DataContractJsonSerializer(typeof(NbrbCur[]));
            DataContractJsonSerializer jsonFormatterNBRBRate = new DataContractJsonSerializer(typeof(NbrbRate));
            NbrbCur[] curArray = (NbrbCur[])jsonFormatterNBRBCurAr.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonCurList)));
            var addedRates = new List<CurrencyRates>();
            foreach (var currency in currencyList)
            {
                for (var currencyDate = Constants.baseDate; currencyDate <= DateTime.Today; currencyDate = currencyDate.AddDays(1))
                {

                    if (await _context.CurrencyRates.Where(x => x.DateRate == currencyDate && x.Currency.Code == currency.Code).SingleOrDefaultAsync() != null)
                        continue;

                    var curNBRB = curArray.SingleOrDefault(x => x.Cur_Abbreviation == currency.Code &&
                                                               DateTime.Parse(x.Cur_DateStart) <= currencyDate &&
                                                               DateTime.Parse(x.Cur_DateEnd) >= currencyDate
                                                               );

                    if (curNBRB == null)
                        continue;

                    string urlRate = Constants.BYN_CURRENCY_RATE_URL + curNBRB.Cur_ID + Constants.BYN_ATTR_DATE + currencyDate;
                    string jsonRate = await ServicesCommonFunctions.SendGetRequestAsync(new Uri(urlRate));
                    NbrbRate curRate = jsonFormatterNBRBRate.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonRate))) as NbrbRate;
                    CurrencyRates currencyRate = new CurrencyRates
                    {
                        BaseCurrencyId = baseCurrency.Id,
                        CurrencyId = currency.Id,
                        DateRate = currencyDate,
                        CurrecyScale = curRate.Cur_Scale,
                        CurrencyRate = (decimal)curRate.Cur_OfficialRate
                    };
                    addedRates.Add(currencyRate);
                }
            }
            if (addedRates.Count>0)
            {
                await _context.AddRangeAsync(addedRates);
                await _context.SaveChangesAsync();
            }
            
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
