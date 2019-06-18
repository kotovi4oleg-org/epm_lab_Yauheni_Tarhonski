using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class ItemService :BaseService, IItemService
    {
        public ItemService(DefaultContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Item>> GetListAsync()
        {
            var defaultContext = _context.Item.Include(i => i.Unit);
            return await defaultContext.AsNoTracking().ToListAsync();
        }
        public async Task<Item> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            Item resultObject;
            if(tracking) resultObject = await _context.Item.Include(x => x.Unit)
                                                           .SingleOrDefaultAsync(m => m.Id == id);
            else resultObject = await _context.Item.Include(x => x.Unit)
                                                   .AsNoTracking()
                                                   .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public async Task AddAsync(Item entity)
        {
            await ServicesCommonFunctions.AddObject(entity, _context);
        }
        public async Task<bool> UpdateAsync(Item entity)
        {
            return await ServicesCommonFunctions.UpdateObject(entity, _context);
        }
        public async Task DeleteAsync(long id)
        {
            await ServicesCommonFunctions.DeleteObject<Item>(id, _context);
        }
        public SelectList GetUnitIds()
        {
            return new SelectList(_context.Unit.AsNoTracking(), "Id", "Name"); 
        }
    }
}
