using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface ICitiesService : IBaseService<City>
    {
        IQueryable<City> GetFiltredCities(string sortOrder, string searchString);
        SelectList GetStatesIds(long countryId);
        SelectList GetCountriesIds();
        List<State> GetStates(long id);
    }
}
