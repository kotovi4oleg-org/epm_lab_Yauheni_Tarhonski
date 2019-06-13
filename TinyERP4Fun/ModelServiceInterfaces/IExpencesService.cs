using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ViewModels;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IExpencesService
    {
        Task<Expences> GetExpenceInfo(long? id);
        Task<ExpencesViewModel> GetFilteredExpences(int? pageNumber, ExpencesViewModel expencesViewModel, string currentUserId, bool adm);
    }
}
