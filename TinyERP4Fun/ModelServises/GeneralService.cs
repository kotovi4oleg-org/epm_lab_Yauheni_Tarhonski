using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class GeneralService: IGeneralService
    {
        private readonly DefaultContext _context;
        public GeneralService(DefaultContext context)
        {
            _context = context;
        }

        public async Task<T> GetObject<T>(long? id) where T : class, IHaveLongId
        {
            return await ServicesCommonFunctions.GetObject<T>(id, _context, true);
        }

    }
}
