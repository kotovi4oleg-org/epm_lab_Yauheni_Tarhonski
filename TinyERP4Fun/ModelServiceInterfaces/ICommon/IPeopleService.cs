using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ViewModels;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IPeopleService : IBaseService<Person>
    {
        Task<bool> UpdateAsync(PersonViewModel personViewModel);
        Task<SelectList> GetUsersIds(long? id);
        SelectList GetCompaniesIds();
        SelectList GetRolesIds();
        SelectList GetRolesNames();
        Task<IList<string>> GetRolesAsync(IdentityUser user);
    }
}
