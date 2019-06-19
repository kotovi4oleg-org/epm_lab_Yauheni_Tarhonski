using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class EmployeesService : BaseService<Employee>, IEmployeesService
    {
        public EmployeesService(DefaultContext context) : base(context)
        {
        }
        public override IQueryable<Employee> GetIQueryable()
        {
            return _context.Employee.Include(x => x.Person)
                                               .Include(x => x.Department)
                                               .Include(x => x.Position)
                                               .OrderBy(x => x.Person.LastName)
                                               .ThenBy(x => x.Person.FirstName);
        }
        public override async Task<Employee> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            Employee resultObject;
            if (tracking)
                resultObject = await _context.Employee.Include(x => x.Person)
                                                      .Include(x => x.Department)
                                                      .Include(x => x.Position)
                                                      .SingleOrDefaultAsync(t => t.Id == id);
            else
                resultObject = await _context.Employee.Include(x => x.Person)
                                                      .Include(x => x.Department)
                                                      .Include(x => x.Position)
                                                      .SingleOrDefaultAsync(t => t.Id == id);
            return resultObject;
        }

        public SelectList GetPersonsIds()
        {
            return ServicesCommonFunctions.AddFirstItem(
                          new SelectList(_context.Person.AsNoTracking(), "Id", "Email"));
        }
        public SelectList GetDepartmentsIds()
        {
            return ServicesCommonFunctions.AddFirstItem(
                          new SelectList(_context.Department.AsNoTracking(), "Id", "Email"));
        }
        public SelectList GetPositionsIds()
        {
            return ServicesCommonFunctions.AddFirstItem(
                          new SelectList(_context.Position.AsNoTracking(), "Id", "Email"));
        }
    }
}
