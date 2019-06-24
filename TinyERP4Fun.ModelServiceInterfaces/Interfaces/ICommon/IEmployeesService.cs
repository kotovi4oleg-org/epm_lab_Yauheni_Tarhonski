using System.Linq;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Interfaces
{
    public interface IEmployeesService : IBaseService<Employee>
    {
        IQueryable<Ids> GetPersonsIds();
        IQueryable<Ids> GetDepartmentsIds();
        IQueryable<Ids> GetPositionsIds();
    }
}
