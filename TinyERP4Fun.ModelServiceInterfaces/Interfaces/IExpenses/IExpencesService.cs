using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Expenses;

namespace TinyERP4Fun.Interfaces
{
    public interface IExpencesService: IBaseService<Expences>
    {
        Task<ExpencesFiltredModel> GetFilteredContentAsync(int? pageNumber, ExpencesFiltredModel expencesFilterModel, string currentUserId, bool adm);
        IQueryable<Ids> GetCurrenciesIds();
        IQueryable<Ids> GetCompaniesIds();
        IQueryable<Ids> GetOurCompaniesIds();
        IQueryable<Ids> GetDocumentTypesIds();
        IQueryable<Ids> GetPersonsIds(string currentUserId);
        IQueryable<Ids> GetUsersIds(string currentUserId);
    }
}
