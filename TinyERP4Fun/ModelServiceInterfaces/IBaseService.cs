using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IBaseService<T> where T: class, IHaveLongId
    {
        IQueryable<T> GetIQueryable();
        Task<IEnumerable<T>> GetListAsync();
        Task<T> GetAsync(long? id, bool tracking = false);
        Task AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}
