using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public class UnitService : BaseService<Unit>, IUnitService
    {
        public UnitService(IDefaultContext context) : base(context)
        {
        }

    }
}
