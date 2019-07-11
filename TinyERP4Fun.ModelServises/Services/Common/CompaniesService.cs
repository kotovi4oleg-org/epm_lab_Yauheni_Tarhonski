using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public class CompaniesService : BaseService<Company>, ICompaniesService
    {
        public CompaniesService(IDefaultContext context) : base(context)
        {
        }

        public override async Task<Company> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            Company resultObject;
            if (tracking)
                resultObject = await _context.Company.Include(x => x.City)
                                                     .Include(x => x.City.State)
                                                     .Include(x => x.City.State.Country)
                                                     .Include(x => x.HeadCompany)
                                                     .Include(x => x.BaseCurrency)
                                                     .SingleOrDefaultAsync(c => c.Id == id);
            else
                resultObject = await _context.Company.Include(x => x.City)
                                                     .Include(x => x.City.State)
                                                     .Include(x => x.City.State.Country)
                                                     .Include(x => x.HeadCompany)
                                                     .Include(x => x.BaseCurrency)
                                                     .AsNoTracking()
                                                     .SingleOrDefaultAsync(c => c.Id == id);
            return resultObject;
        }

        public IQueryable<Company> GetFiltredContent(string sortOrder, string searchString)
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
        public IQueryable<Ids> GetCitiesIds(long stateId)
        {
            return _context.City.Where(x => x.StateId == stateId).AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetStatesIds(long countryId)
        {
            return _context.State.Where(x => x.CountryId == countryId).AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetCountriesIds()
        {
            return _context.Country.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetCompaniesIds()
        {
            return _context.Company.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetCurrenciesIds()
        {
            return _context.Currency.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public async Task<IEnumerable<City>> GetCities(long stateId)
        {
            return await _context.City.Where(x => x.StateId == stateId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<State>> GetStates(long countryId)
        {
            return await _context.State.Where(x => x.CountryId == countryId).AsNoTracking().ToListAsync();
        }
    }
}
