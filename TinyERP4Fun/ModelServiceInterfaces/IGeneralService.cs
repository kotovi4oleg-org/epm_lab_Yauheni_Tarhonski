using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IGeneralService
    {
        Task<T> GetObject<T>(long? id) where T : class, IHaveLongId;
        void AddImage<T>(ref T entity, IList<IFormFile> files) where T:IHaveImage;
        FileStreamResult GetImage<T>(long? id) where T : class, IHaveLongId, IHaveImage;
    }
}
