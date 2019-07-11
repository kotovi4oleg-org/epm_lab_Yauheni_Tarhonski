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
    public class EmployeesService : BaseService<Employee>, IEmployeesService
    {
        public EmployeesService(IDefaultContext context) : base(context)
        {
        }
        public override IQueryable<Employee> GetIQueryable()
        {
            return _context.Employee.Include(x => x.Person)
                                               .Include(x => x.Department)
                                               .Include(x => x.Position)
                                               ;
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

        public IQueryable<Ids> GetPersonsIds()
        {
            return _context.Person.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetDepartmentsIds()
        {
            return _context.Department.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetPositionsIds()
        {
            return _context.Position.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
    }
}
