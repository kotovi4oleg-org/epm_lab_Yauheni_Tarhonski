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
    public class WarehouseService : BaseService, IWarehouseService
    {
        public WarehouseService(DefaultContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Warehouse>> GetListAsync()
        {
            var defaultContext = _context.Warehouse;
            return await defaultContext.AsNoTracking().ToListAsync();
        }
        public async Task<Warehouse> GetAsync(long? id, bool tracking = false)
        {
            return await ServicesCommonFunctions.GetObject<Warehouse>(id,_context,tracking);
        }
        public async Task AddAsync(Warehouse entity)
        {
            await ServicesCommonFunctions.AddObject(entity, _context);
        }
        public async Task<bool> UpdateAsync(Warehouse entity)
        {
            return await ServicesCommonFunctions.UpdateObject(entity, _context);
        }
        public async Task DeleteAsync(long id)
        {
            await ServicesCommonFunctions.DeleteObject<Warehouse>(id, _context);
        }
    }
}
