using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public class StatesService : BaseService<State>, IStatesService
    {
        public StatesService(DefaultContext context) : base(context)
        {
        }

        public override IQueryable<State> GetIQueryable()
        {
            return _context.State.Include(x => x.Country).OrderBy(x => x.Name);
        }
        public IQueryable<Ids> GetCountriesIds()
        {
            return _context.Country.AsNoTracking().Select(x=>new Ids(x.Id.ToString(),x.Name));
        }
        public override async Task<State> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            State resultObject;
            if (tracking)
                resultObject = await _context.State.Include(x => x.Country)
                                                   .SingleOrDefaultAsync(t => t.Id == id);
            else
                resultObject = await _context.State.Include(x => x.Country)
                                                   .AsNoTracking()
                                                   .SingleOrDefaultAsync(t => t.Id == id);
            return resultObject;
            
        }

    }
}
