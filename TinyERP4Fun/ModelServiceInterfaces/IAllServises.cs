using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IAllServises<T> where T: class
    {
        Task<T> GetAsync(long? id, bool tracking = false);
        Task AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}
