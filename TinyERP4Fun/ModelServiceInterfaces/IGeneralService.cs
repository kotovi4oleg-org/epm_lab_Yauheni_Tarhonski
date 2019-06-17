using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyERP4Fun.Models;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IGeneralService
    {
        Task<T> GetObject<T>(long? id) where T : class, IHaveLongId;
    }
}
