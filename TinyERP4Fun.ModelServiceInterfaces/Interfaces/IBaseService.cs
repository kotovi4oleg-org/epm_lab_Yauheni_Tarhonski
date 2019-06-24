using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TinyERP4Fun.Interfaces
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
