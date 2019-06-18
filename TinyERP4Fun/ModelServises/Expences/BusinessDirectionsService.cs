using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class BusinessDirectionsService : BaseService, IBusinessDirectionsService
    {
        public BusinessDirectionsService(DefaultContext context) : base(context)
        {
        }
        public async Task<IEnumerable<BusinessDirection>> GetListAsync()
        {
            var defaultContext = _context.BusinessDirection;
            return await defaultContext.AsNoTracking().ToListAsync();
        }
        public async Task<BusinessDirection> GetAsync(long? id, bool tracking = false)
        {
            return await ServicesCommonFunctions.GetObject<BusinessDirection>(id, _context, tracking);
        }
        public async Task AddAsync(BusinessDirection entity)
        {
            await ServicesCommonFunctions.AddObject(entity, _context);
        }
        public async Task<bool> UpdateAsync(BusinessDirection entity)
        {
            return await ServicesCommonFunctions.UpdateObject(entity, _context);
        }
        public async Task DeleteAsync(long id)
        {
            await ServicesCommonFunctions.DeleteObject<BusinessDirection>(id, _context);
        }
    }
}
