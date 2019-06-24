using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public class PositionsService : BaseService<Position>, IPositionsService
    {
        public PositionsService(DefaultContext context) : base(context)
        {
        }
    }
}
