using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TinyERP4Fun.ModelServises
{
    public abstract class BaseService<T> : IBaseService<T> where T: class, IHaveLongId 
    {
        protected readonly IDefaultContext _context;

        public BaseService(IDefaultContext context)
        {
            _context = context;
        }
        public virtual IQueryable<T> GetIQueryable()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public virtual async Task<IEnumerable<T>> GetListAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            T resultObject;
            if (tracking)
                resultObject = await _context.Set<T>().SingleOrDefaultAsync(t => t.Id == id);
            else
                resultObject = await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
            return resultObject;
        }
        public virtual async Task AddAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            //return await ServicesCommonFunctions.UpdateObject(entity, _context);
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(entity.Id, _context)) return false;
                throw;
            }
        }
        public virtual async Task DeleteAsync(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public static bool EntityExists(long id, DefaultContext _context)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }
    }
}
