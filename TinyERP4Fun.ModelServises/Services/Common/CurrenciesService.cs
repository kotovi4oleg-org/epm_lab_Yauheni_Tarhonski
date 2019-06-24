using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public class CurrenciesService : BaseService<Currency>, ICurrenciesService
    {
        public CurrenciesService(DefaultContext context) : base(context)
        {
        }
    }
}
