using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Interfaces
{
    public interface IPeopleService : IBaseService<Person>
    {
        Task<bool> UpdateAsync(Person person, IEnumerable<string> selectedRoles);
        Task<IQueryable<Ids>> GetUsersIds(long? id);
        IQueryable<Ids> GetCompaniesIds();
        IQueryable<Ids> GetRolesIds();
        IQueryable<Ids> GetRolesNames();
        Task<IEnumerable<string>> GetRolesAsync(IdentityUser user);
    }
}
