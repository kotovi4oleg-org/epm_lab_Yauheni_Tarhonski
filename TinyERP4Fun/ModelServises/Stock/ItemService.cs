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
    public class ItemService: IItemService
    {
        private readonly DefaultContext _context;

        public ItemService(DefaultContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Item>> GetItemsList()
        {
            var defaultContext = _context.Item.Include(i => i.Unit);
            return await defaultContext.ToListAsync();
        }
        public async Task<Item> GetItem(long? id, bool tracking = false)
        {
            if (id == null) return null;
            Item resultObject;
            if(tracking) resultObject = await _context.Item.Include(x => x.Unit)
                                                           .SingleOrDefaultAsync(m => m.Id == id);
            else resultObject = await _context.Item.AsNoTracking().Include(x => x.Unit)
                                                                  .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public async Task AddItem(Item item)
        {
            await ServicesCommonFunctions.AddObject(item, _context);
        }
        public async Task<bool> UpdateItem(Item item)
        {
            return await ServicesCommonFunctions.UpdateObject(item, _context);
        }
        public async Task DeleteItem(long id)
        {
            await ServicesCommonFunctions.DeleteObject<Item>(id, _context);
        }

        public SelectList GetUnitIds()
        {
            return new SelectList(_context.Unit, "Id", "Name"); 
        }
    }
}
