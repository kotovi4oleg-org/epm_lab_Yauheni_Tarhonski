using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface ICommonService
    {
        //Task<T> GetObject<T>(long? id) where T : class, IHaveLongId;
        Task UpdateBYNVoid();
        Task<Employee> GetEmployeeInfo(long? id);
        Task<CurrencyRates> GetCurrencyRatesInfo(long? id);
        Task<Person> GetPersonInfo(long? id);
        Task<State> GetStateInfo(long? id);
        Task<City> GetCityInfo(long? id);
        Task<Company> GetCompanyInfo(long? id);
        IQueryable<City> GetFiltredCities(string sortOrder, string searchString);
        IQueryable<Company> GetFiltredCompanies(string sortOrder, string searchString);
    }
}
