using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class CommonService : ICommonService
    {
        private readonly DefaultContext _context;

        public CommonService(DefaultContext context)
        {
            _context = context;
        }

        
        [DataContract]
        internal class NbrbCur
        {
            [DataMember]
            // Microsoft.CodeAnalysis.CSharp.Workspaces ругается варнингами на непроинициализированные поля, поэтому.... 
            // проинициализирую их явно, хоть и неявное поведение мне было понятно и оно меня устраивало.
            public int Cur_ID = default;
            [DataMember]
            public string Cur_Abbreviation = default;
            [DataMember]
            public int Cur_Scale = default;
            [DataMember]
            public string Cur_DateStart = default;
            [DataMember]
            public string Cur_DateEnd = default;
        }
        [DataContract]
        internal class NbrbRate
        {
            [DataMember]
            public int Cur_Scale = default;
            [DataMember]
            public double Cur_OfficialRate = default;
        }
        private static async Task<string> SendGetRequestAsync(Uri url)
        {
            var content = new MemoryStream();
            try //http
            {
                var webReq = WebRequest.Create(url);
                Task<WebResponse> responseTask = webReq.GetResponseAsync();
                using (WebResponse response = await responseTask)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(content);
                    }
                }
            }
            catch //https
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var webReq = WebRequest.Create(url);
                Task<WebResponse> responseTask = webReq.GetResponseAsync();
                using (WebResponse response = await responseTask)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(content);
                    }
                }
            }
            return Encoding.ASCII.GetString(content.ToArray());
        }
        public async Task UpdateBYNVoid()
        {
            Currency baseCurrency = _context.Currency.Where(x => x.Code == Constants.BYNCode).FirstOrDefault();
            var currencyList = _context.Currency.Where(x => x.Code != Constants.BYNCode && x.Active);
            string jsonCurList = await SendGetRequestAsync(new Uri(Constants.BYN_CURRENCY_LIST_URL));
            DataContractJsonSerializer jsonFormatterNBRBCurAr = new DataContractJsonSerializer(typeof(NbrbCur[]));
            DataContractJsonSerializer jsonFormatterNBRBRate = new DataContractJsonSerializer(typeof(NbrbRate));
            NbrbCur[] curArray = (NbrbCur[])jsonFormatterNBRBCurAr.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonCurList)));

            foreach (var currency in currencyList)
            {
                for (var currencyDate = Constants.baseDate; currencyDate <= DateTime.Today; currencyDate = currencyDate.AddDays(1))
                {

                    if (_context.CurrencyRates.Where(x => x.DateRate == currencyDate && x.Currency.Code == currency.Code).FirstOrDefault() != null)
                        continue;

                    var curNBRB = curArray.FirstOrDefault(x => x.Cur_Abbreviation == currency.Code &&
                                                               DateTime.Parse(x.Cur_DateStart) <= currencyDate &&
                                                               DateTime.Parse(x.Cur_DateEnd) >= currencyDate
                                                               );

                    if (curNBRB == null)
                        continue;

                    string urlRate = Constants.BYN_CURRENCY_RATE_URL + curNBRB.Cur_ID + Constants.BYN_ATTR_DATE + currencyDate;
                    string jsonRate = await SendGetRequestAsync(new Uri(urlRate));
                    NbrbRate curRate = jsonFormatterNBRBRate.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonRate))) as NbrbRate;
                    CurrencyRates currencyRate = new CurrencyRates
                    {
                        BaseCurrencyId = baseCurrency.Id,
                        CurrencyId = currency.Id,
                        DateRate = currencyDate,
                        CurrecyScale = curRate.Cur_Scale,
                        CurrencyRate = (decimal)curRate.Cur_OfficialRate
                    };
                    _context.Add(currencyRate);
                }
            }
            await _context.SaveChangesAsync();
        }

        public IQueryable<City> GetFiltredCities(string sortOrder, string searchString)
        {
            IQueryable<City> result = _context.City.Include(x => x.State).Include(x => x.State.Country);

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.Name.Contains(searchString)
                                       || x.State.Name.Contains(searchString)
                                       || x.State.Country.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(x => x.Name);
                    break;
                case "state":
                    result = result.OrderBy(x => x.State.Name).ThenBy(x => x.Name);
                    break;
                case "state_desc":
                    result = result.OrderByDescending(x => x.State.Name).ThenBy(x => x.Name);
                    break;
                case "country":
                    result = result.OrderBy(x => x.State.Country.Name).ThenBy(x => x.Name);
                    break;
                case "country_desc":
                    result = result.OrderByDescending(x => x.State.Country.Name).ThenBy(x => x.Name);
                    break;
                default:
                    result = result.OrderBy(x => x.Name);
                    break;
            }
            return result;
        }
        public IQueryable<Company> GetFiltredCompanies(string sortOrder, string searchString)
        {
            IQueryable<Company> result = _context.Company
                                                 .Include(x => x.City)
                                                 .Include(x => x.City.State)
                                                 .Include(x => x.City.State.Country);

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.Name.Contains(searchString)
                                       || x.City.Name.Contains(searchString)
                                       || x.City.State.Name.Contains(searchString)
                                       || x.City.State.Country.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(x => x.Name);
                    break;
                case "city":
                    result = result.OrderBy(x => x.City.Name).ThenBy(x => x.Name);
                    break;
                case "city_desc":
                    result = result.OrderByDescending(x => x.City.Name).ThenBy(x => x.Name);
                    break;
                case "state":
                    result = result.OrderBy(x => x.City.State.Name).ThenBy(x => x.Name);
                    break;
                case "state_desc":
                    result = result.OrderByDescending(x => x.City.State.Name).ThenBy(x => x.Name);
                    break;
                case "country":
                    result = result.OrderBy(x => x.City.State.Country.Name).ThenBy(x => x.Name);
                    break;
                case "country_desc":
                    result = result.OrderByDescending(x => x.City.State.Country.Name).ThenBy(x => x.Name);
                    break;
                default:
                    result = result.OrderBy(x => x.Name);
                    break;
            }
            return result;
        }
        public async Task<Employee> GetEmployeeInfo(long? id)
        {
            if (id == null) return null;

            var resultObject = await _context.Employee.Include(x => x.Person)
                                                      .Include(x => x.Department)
                                                      .Include(x => x.Position)
                                                      .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public async Task<CurrencyRates> GetCurrencyRatesInfo(long? id)
        {
            if (id == null) return null;

            var resultObject = await _context.CurrencyRates
                                             .Include(c => c.BaseCurrency)
                                             .Include(c => c.Currency)
                                             .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public async Task<Person> GetPersonInfo(long? id)
        {
            if (id == null) return null;
            var resultObject = await _context.Person.Include(x => x.User)
                                                    .Include(x => x.Company)
                                                    .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public async Task<State> GetStateInfo(long? id)
        {
            if (id == null) return null;
            var resultObject = await _context.State
                                                  .Include(x => x.Country)
                                                  .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public async Task<City> GetCityInfo(long? id)
        {
            if (id == null) return null;
            var resultObject = await _context.City.Include(x => x.State)
                                                  .Include(x => x.State.Country)
                                                  .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public async Task<Company> GetCompanyInfo(long? id)
        {
            if (id == null) return null;
            var resultObject = await _context.Company.Include(x => x.City)
                                                     .Include(x => x.City.State)
                                                     .Include(x => x.City.State.Country)
                                                     .Include(x => x.HeadCompany)
                                                     .Include(x => x.BaseCurrency)
                                                     .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
    }
}
