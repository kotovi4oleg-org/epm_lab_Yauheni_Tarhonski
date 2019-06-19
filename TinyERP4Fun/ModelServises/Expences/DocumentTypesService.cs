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
    public class DocumentTypesService : BaseService<DocumentType>, IDocumentTypesService
    {
        public DocumentTypesService(DefaultContext context) : base(context)
        {
        }
    }
}
