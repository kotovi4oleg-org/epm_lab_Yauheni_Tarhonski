using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public abstract class BaseService<T> : IBaseService<T> where T: class, IHaveLongId 
    {
        protected readonly DefaultContext _context;

        public BaseService(DefaultContext context)
        {
            _context = context;
        }
        public virtual IQueryable<T> GetIQueryable()
        {
            return ServicesCommonFunctions.GetIQueryable<T>(_context);
        }
        public virtual async Task<IEnumerable<T>> GetListAsync()
        {
            return await ServicesCommonFunctions.GetListAsync<T>(_context);
        }

        public virtual async Task<T> GetAsync(long? id, bool tracking = false)
        {
            return await ServicesCommonFunctions.GetObject<T>(id, _context, tracking);
        }
        public virtual async Task AddAsync(T entity)
        {
            await ServicesCommonFunctions.AddObject(entity, _context);
        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            return await ServicesCommonFunctions.UpdateObject(entity, _context);
        }
        public virtual async Task DeleteAsync(long id)
        {
            await ServicesCommonFunctions.DeleteObject<T>(id, _context);
        }
    }
}
