using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<T> GetObject<T> (long? id) where T : class, IHaveLongId
        {
            if (id == null) return null;

            T resultObject = await _context.Set<T>()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (resultObject == null) return null;

            return resultObject;
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
