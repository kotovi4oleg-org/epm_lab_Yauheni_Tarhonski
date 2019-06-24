using System.Linq;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Interfaces
{
    public interface IStatesService : IBaseService<State>
    {
        IQueryable<Ids> GetCountriesIds();
    }
}
