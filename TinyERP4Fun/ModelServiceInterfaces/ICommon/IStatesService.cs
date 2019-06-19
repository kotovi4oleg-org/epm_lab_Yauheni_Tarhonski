using Microsoft.AspNetCore.Mvc.Rendering;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IStatesService : IBaseService<State>
    {
        SelectList GetCountriesIds();
    }
}
