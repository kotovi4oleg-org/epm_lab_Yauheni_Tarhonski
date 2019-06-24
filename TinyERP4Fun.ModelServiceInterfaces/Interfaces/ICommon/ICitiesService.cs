using System.Collections.Generic;
using System.Linq;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Interfaces
{
    public interface ICitiesService : IBaseService<City>
    {
        IQueryable<City> GetFiltredCities(string sortOrder, string searchString);
        IQueryable<Ids> GetStatesIds(long countryId);
        IQueryable<Ids> GetCountriesIds();
        IEnumerable<State> GetStates(long id);
    }
}
