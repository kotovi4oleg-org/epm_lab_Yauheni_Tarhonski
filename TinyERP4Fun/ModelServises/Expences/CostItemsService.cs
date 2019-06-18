using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class CostItemsService : BaseService, ICostItemsService
    {
        public CostItemsService(DefaultContext context) : base(context)
        {
        }
        public async Task<IEnumerable<CostItem>> GetListAsync()
        {
            var defaultContext = _context.CostItem;
            return await defaultContext.AsNoTracking().ToListAsync();
        }
        public async Task<CostItem> GetAsync(long? id, bool tracking = false)
        {
            return await ServicesCommonFunctions.GetObject<CostItem>(id, _context, tracking);
        }
        public async Task AddAsync(CostItem entity)
        {
            await ServicesCommonFunctions.AddObject(entity, _context);
        }
        public async Task<bool> UpdateAsync(CostItem entity)
        {
            return await ServicesCommonFunctions.UpdateObject(entity, _context);
        }
        public async Task DeleteAsync(long id)
        {
            await ServicesCommonFunctions.DeleteObject<CostItem>(id, _context);
        }
    }
}
