using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class UnitServise : BaseService, IUnitServise
    {
        public UnitServise(DefaultContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Unit>> GetListAsync()
        {
            var defaultContext = _context.Unit;
            return await defaultContext.AsNoTracking().ToListAsync();
        }
        public async Task<Unit> GetAsync(long? id, bool tracking = false)
        {
            return await ServicesCommonFunctions.GetObject<Unit>(id, _context, tracking);
        }
        public async Task AddAsync(Unit entity)
        {
            await ServicesCommonFunctions.AddObject(entity, _context);
        }
        public async Task<bool> UpdateAsync(Unit entity)
        {
            return await ServicesCommonFunctions.UpdateObject(entity, _context);
        }
        public async Task DeleteAsync(long id)
        {
            await ServicesCommonFunctions.DeleteObject<Unit>(id, _context);
        }
    }
}
