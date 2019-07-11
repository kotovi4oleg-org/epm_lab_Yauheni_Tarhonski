using Microsoft.AspNetCore.Identity;
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
    public class PeopleService : BaseService<Person>, IPeopleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public PeopleService(DefaultContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) : base(context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public override IQueryable<Person> GetIQueryable()
        {
            return _context.Set<Person>().Include(x => x.User)
                                  .Include(x => x.Company)
                                  .OrderBy(x => x.LastName)
                                  .ThenBy(x => x.FirstName)
                                  ;
        }
        public override async Task AddAsync(Person person)
        {
            _context.Add(person);
            if (person.IsEmployee)
            {
                Employee employee = new Employee { Person = person };
                _context.Add(employee);
            }
            await _context.SaveChangesAsync();
        }
        
        public async Task<bool> UpdateAsync(Person person, IEnumerable<string> selectedRoles)
        {
            try
            {
                if (person.UserId != null)
                {
                    var user = await _userManager.FindByIdAsync(person.UserId);
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    if (selectedRoles != null)
                        await _userManager.AddToRolesAsync(user, selectedRoles);
                }
                _context.Update(person);
                if (person.IsEmployee && _context.Employee.Where(x => x.PersonId == person.Id).Count() == 0)
                {
                    Employee employee = new Employee { Person = person };
                    _context.Add(employee);
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(person.Id, _context)) return false;
                throw;
            }
            return true;

        }
        public override async Task<Person> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            Person resultObject;
            if (tracking)
                resultObject = await _context.Set<Person>().Include(x => x.User)
                                                    .Include(x => x.Company)
                                                    .SingleOrDefaultAsync(t => t.Id == id);
            else
                resultObject = await _context.Set<Person>().Include(x => x.User)
                                                    .Include(x => x.Company)
                                                    .SingleOrDefaultAsync(t => t.Id == id);
            return resultObject;
        }
        public async Task<IQueryable<Ids>> GetUsersIds(long? id)
        {
            var defaultAdmin = new List<IdentityUser> { await _userManager.FindByNameAsync(Constants.defaultAdminName) };
            var usedUsers = _context.Set<Person>().Where(x => x.User != null && x.Id != id).Select(x => x.User);
            var result = _userManager.Users.Except(defaultAdmin).Except(usedUsers).Select(x => new Ids(x.Id, x.Email));
            return result;
        }
        public IQueryable<Ids> GetCompaniesIds()
        {
            return _context.Set<Company>().AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetRolesIds()
        {
            return _roleManager.Roles.Select(x => new Ids(x.Id, x.Name));
        }
        public IQueryable<Ids> GetRolesNames()
        {
            return _roleManager.Roles.Select(x=>new Ids(x.Name, x.Name));
        }

        public async Task<IEnumerable<string>> GetRolesAsync(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
