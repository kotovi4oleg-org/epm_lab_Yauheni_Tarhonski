using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;

namespace TinyERP4Fun.ModelServises
{
    public abstract class BaseService
    {
        protected readonly DefaultContext _context;

        public BaseService(DefaultContext context)
        {
            _context = context;
        }
    }
}
