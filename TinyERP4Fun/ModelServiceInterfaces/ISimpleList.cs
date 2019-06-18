using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface ISimpleList<T> where T: class
    {
        Task<IEnumerable<T>> GetListAsync();
    }
}
