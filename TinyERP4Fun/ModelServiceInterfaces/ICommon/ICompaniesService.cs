using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface ICompaniesService : IBaseService<Company>
    {
        IQueryable<Company> GetFiltredContent(string sortOrder, string searchString);
        SelectList GetCitiesIds(long stateId);
        SelectList GetStatesIds(long countryId);
        SelectList GetCountriesIds();
        SelectList GetCompaniesIds();
        SelectList GetCurrenciesIds();
        List<City> GetCities(long stateId);
        List<State> GetStates(long countryId);
    }
}
