using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public class CitiesService : BaseService<City>, ICitiesService
    {
        public CitiesService(DefaultContext context) : base(context)
        {
        }
        public IQueryable<City> GetFiltredCities(string sortOrder, string searchString)
        {
            IQueryable<City> result = _context.Set<City>().Include(x => x.State).Include(x => x.State.Country);

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
        public override async Task<IEnumerable<City>> GetListAsync()
        {
            var defaultContext = _context.Set<City>().Include(x => x.State)
                                              .Include(x => x.State.Country);
            return await defaultContext.AsNoTracking().ToListAsync();
        }
        public override async Task<City> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            City resultObject;
            if (tracking)
                resultObject = await _context.Set<City>()
                                             .Include(x => x.State)
                                             .Include(x => x.State.Country)
                                             .SingleOrDefaultAsync(t => t.Id == id);
            else
                resultObject = await _context.Set<City>()
                                             .Include(x => x.State)
                                             .Include(x => x.State.Country)
                                             .AsNoTracking()
                                             .SingleOrDefaultAsync(t => t.Id == id);
            return resultObject;
        }
        public IQueryable<Ids> GetStatesIds(long countryId)
        {
            return _context.Set<State>().Where(x => x.CountryId == countryId).AsNoTracking().Select(x=>new Ids(x.Id.ToString(),x.Name));
        }
        public IQueryable<Ids> GetCountriesIds()
        {
            return _context.Set<Country>().AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IEnumerable<State> GetStates(long countryId)
        {
            return _context.Set<State>().Where(x => x.CountryId == countryId).AsNoTracking().ToList();
        }
    }
}
