using Microsoft.AspNetCore.Mvc.Rendering;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IEmployeesService : IBaseService<Employee>
    {
        SelectList GetPersonsIds();
        SelectList GetDepartmentsIds();
        SelectList GetPositionsIds();
    }
}
