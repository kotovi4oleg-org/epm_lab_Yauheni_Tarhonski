using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Interfaces
{
    public interface ICompaniesService : IBaseService<Company>
    {
        IQueryable<Company> GetFiltredContent(string sortOrder, string searchString);
        IQueryable<Ids> GetCitiesIds(long stateId);
        IQueryable<Ids> GetStatesIds(long countryId);
        IQueryable<Ids> GetCountriesIds();
        IQueryable<Ids> GetCompaniesIds();
        IQueryable<Ids> GetCurrenciesIds();
        Task<IEnumerable<City>> GetCities(long stateId);
        Task<IEnumerable<State>> GetStates(long countryId);
    }
}
