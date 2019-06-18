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
    public class DocumentTypesService : BaseService, IDocumentTypesService
    {
        public DocumentTypesService(DefaultContext context) : base(context)
        {
        }
        public async Task<IEnumerable<DocumentType>> GetListAsync()
        {
            var defaultContext = _context.DocumentType;
            return await defaultContext.AsNoTracking().ToListAsync();
        }
        public async Task<DocumentType> GetAsync(long? id, bool tracking = false)
        {
            return await ServicesCommonFunctions.GetObject<DocumentType>(id, _context, tracking);
        }
        public async Task AddAsync(DocumentType entity)
        {
            await ServicesCommonFunctions.AddObject(entity, _context);
        }
        public async Task<bool> UpdateAsync(DocumentType entity)
        {
            return await ServicesCommonFunctions.UpdateObject(entity, _context);
        }
        public async Task DeleteAsync(long id)
        {
            await ServicesCommonFunctions.DeleteObject<DocumentType>(id, _context);
        }
    }
}
