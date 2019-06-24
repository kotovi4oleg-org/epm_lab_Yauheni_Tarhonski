using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public class ItemService : BaseService<Item>, IItemService
    {
        public ItemService(DefaultContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Item>> GetListAsync()
        {
            var defaultContext = _context.Item.Include(x => x.Unit);
            return await defaultContext.AsNoTracking().ToListAsync();
        }
        public override async Task<Item> GetAsync(long? id, bool tracking = false)
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
        public IQueryable<Ids> GetUnitsIds()
        {
            return _context.Unit.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name ));
        }
    }
}
