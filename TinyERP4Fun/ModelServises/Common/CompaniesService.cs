using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class CompaniesService : BaseService<Company>, ICompaniesService
    {
        public CompaniesService(DefaultContext context) : base(context)
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
        public SelectList GetCitiesIds(long stateId)
        {
            return ServicesCommonFunctions.AddFirstItem(
                                    new SelectList(_context.City.Where(x => x.StateId == stateId).AsNoTracking(), "Id", "Name"));
        }
        public SelectList GetStatesIds(long countryId)
        {
            return ServicesCommonFunctions.AddFirstItem(
                                    new SelectList(_context.State.Where(x => x.CountryId == countryId).AsNoTracking(), "Id", "Name"));
        }
        public SelectList GetCountriesIds()
        {
            return ServicesCommonFunctions.AddFirstItem(
                          new SelectList(_context.Country.AsNoTracking(), "Id", "Name"));
        }
        public SelectList GetCompaniesIds()
        {
            return ServicesCommonFunctions.AddFirstItem(
                          new SelectList(_context.Company.AsNoTracking(), "Id", "Name"));
        }
        public SelectList GetCurrenciesIds()
        {
            return ServicesCommonFunctions.AddFirstItem(
                          new SelectList(_context.Currency.AsNoTracking(), "Id", "Name"));
        }
        public List<City> GetCities(long stateId)
        {
            return _context.City.Where(x => x.StateId == stateId).AsNoTracking().ToList();
        }
        public List<State> GetStates(long countryId)
        {
            return _context.State.Where(x => x.CountryId == countryId).AsNoTracking().ToList();
        }
    }
}
