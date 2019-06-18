using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ViewModels;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IExpencesService: IAllServises<Expences>
    {
        Task<ExpencesViewModel> GetFilteredContentAsync(int? pageNumber, ExpencesViewModel expencesViewModel, string currentUserId, bool adm);
        SelectList GetCurrenciesIds();
        SelectList GetCompaniesIds();
        SelectList GetOurCompaniesIds();
        SelectList GetDocumentTypesIds();
        SelectList GetPersonsIds(string currentUserId);
        SelectList GetUsersIds(string currentUserId);
    }
}
