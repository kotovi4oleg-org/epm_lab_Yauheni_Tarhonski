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
    public class DepartmentsService : BaseService<Department>, IDepartmentsService
    {
        public DepartmentsService(DefaultContext context) : base(context)
        {
        }
    }
}
