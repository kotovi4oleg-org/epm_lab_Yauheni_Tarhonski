using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.Interfaces;


namespace TinyERP4Fun.ModelServises
{
    public class ExpencesService : BaseService<Expences>, IExpencesService
    {
        public ExpencesService(IDefaultContext context) : base(context)
        {
        }
        public override async Task<Expences> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            if (tracking)
                return await _context.Expences.Include(e => e.DocumentType)
                                                      .Include(e => e.Company)
                                                      .Include(e => e.Currency)
                                                      .Include(e => e.OurCompany)
                                                      .Include(e => e.Person)
                                                      .Include(e => e.User)
                                                      .SingleOrDefaultAsync(m => m.Id == id);
            else
                return await _context.Expences.Include(e => e.DocumentType)
                                                      .Include(e => e.Company)
                                                      .Include(e => e.Currency)
                                                      .Include(e => e.OurCompany)
                                                      .Include(e => e.Person)
                                                      .Include(e => e.User)
                                                      .AsNoTracking()
                                                      .SingleOrDefaultAsync(m => m.Id == id);
        }
        public async Task<ExpencesFiltredModel> GetFilteredContentAsync(int? pageNumber, ExpencesFiltredModel expencesFilterModel, string currentUserId, bool adm)
        {
            IQueryable<Expences> defaultContext = _context.Expences.Include(e => e.DocumentType)
                                                                   .Include(e => e.Company)
                                                                   .Include(e => e.Currency)
                                                                   .Include(e => e.OurCompany)
                                                                   .Include(e => e.Person)
                                                                   .Include(e => e.User);
            string total = null;
            if (expencesFilterModel.CurrencyFilter != null)
            {
                var currencyFilter = expencesFilterModel.CurrencyFilter;
                if (currencyFilter.Count != 0)
                {
                    defaultContext = defaultContext.Where(x => currencyFilter.Contains(x.CurrencyId));
                    if (currencyFilter.Count == 1)
                        total = defaultContext.Sum(x => x.AmountOfPayment).ToString();
                }
            }

            if (expencesFilterModel.CompanyFilter != null && expencesFilterModel.CompanyFilter.Count != 0)
                defaultContext = defaultContext.Where(x => expencesFilterModel.CompanyFilter.Contains(x.CompanyId));
            if (expencesFilterModel.OurCompanyFilter != null && expencesFilterModel.OurCompanyFilter.Count != 0)
                defaultContext = defaultContext.Where(x => expencesFilterModel.OurCompanyFilter.Contains(x.OurCompanyId));

            if (expencesFilterModel.FromFilter != null)
                defaultContext = defaultContext.Where(x => x.ApprovedPaymentDate >= expencesFilterModel.FromFilter ||
                                                           x.DesiredPaymentDate >= expencesFilterModel.FromFilter);
            if (expencesFilterModel.ToFilter != null)
                defaultContext = defaultContext.Where(x => x.ApprovedPaymentDate <= expencesFilterModel.ToFilter ||
                                                           x.DesiredPaymentDate <= expencesFilterModel.ToFilter);
            if (!expencesFilterModel.AdmFilter) defaultContext = defaultContext.Where(x => x.Person.UserId == currentUserId);

            expencesFilterModel.Expences = await PaginatedList<Expences>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize);
            expencesFilterModel.Total = total;
            return expencesFilterModel;
        }
        public IQueryable<Ids> GetCurrenciesIds()
        {
            return _context.Currency.Where(x => x.Active).AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetCompaniesIds()
        {
            return _context.Expences.Select(x => x.Company).Distinct().AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetOurCompaniesIds()
        {
            return _context.Company.Where(x => x.OurCompany).AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetDocumentTypesIds()
        {
            return _context.DocumentType.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetPersonsIds(string currentUserId)
        {
            return _context.Person.Where(x => x.UserId == currentUserId && x.UserId != null).AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        public IQueryable<Ids> GetUsersIds(string currentUserId)
        {
            return _context.Users.Where(x => x.Id == currentUserId).AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Email));
        }
    }
}
