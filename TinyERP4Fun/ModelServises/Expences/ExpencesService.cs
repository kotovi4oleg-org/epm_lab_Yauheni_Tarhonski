using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ModelServiceInterfaces;
using TinyERP4Fun.ViewModels;

namespace TinyERP4Fun.ModelServises
{
    public class ExpencesService : BaseService, IExpencesService
    {
        public ExpencesService(DefaultContext context) : base(context)
        {
        }
        public async Task<Expences> GetAsync(long? id, bool tracking = false)
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
        public async Task AddAsync(Expences entity)
        {
            await ServicesCommonFunctions.AddObject(entity, _context);
        }
        public async Task<bool> UpdateAsync(Expences entity)
        {
            return await ServicesCommonFunctions.UpdateObject(entity, _context);
        }
        public async Task DeleteAsync(long id)
        {
            await ServicesCommonFunctions.DeleteObject<Expences>(id, _context);
        }
        public async Task<ExpencesViewModel> GetFilteredContentAsync(int? pageNumber, ExpencesViewModel expencesViewModel, string currentUserId, bool adm)
        {
            IQueryable<Expences> defaultContext = _context.Expences.Include(e => e.DocumentType)
                                                                   .Include(e => e.Company)
                                                                   .Include(e => e.Currency)
                                                                   .Include(e => e.OurCompany)
                                                                   .Include(e => e.Person)
                                                                   .Include(e => e.User);
            string total = null;
            if (expencesViewModel.CurrencyFilter != null)
            {
                var currencyFilter = expencesViewModel.CurrencyFilter;
                if (currencyFilter.Count != 0)
                {
                    defaultContext = defaultContext.Where(x => currencyFilter.Contains(x.CurrencyId));
                    if (currencyFilter.Count == 1)
                        total = Localization.currentLocalizatin["Amount of Expenses"] + ": " + defaultContext.Sum(x => x.AmountOfPayment).ToString();
                }
            }

            if (expencesViewModel.CompanyFilter != null && expencesViewModel.CompanyFilter.Count != 0)
                defaultContext = defaultContext.Where(x => expencesViewModel.CompanyFilter.Contains(x.CompanyId));
            if (expencesViewModel.OurCompanyFilter != null && expencesViewModel.OurCompanyFilter.Count != 0)
                defaultContext = defaultContext.Where(x => expencesViewModel.OurCompanyFilter.Contains(x.OurCompanyId));

            if (expencesViewModel.FromFilter != null)
                defaultContext = defaultContext.Where(x => x.ApprovedPaymentDate >= expencesViewModel.FromFilter ||
                                                           x.DesiredPaymentDate >= expencesViewModel.FromFilter);
            if (expencesViewModel.ToFilter != null)
                defaultContext = defaultContext.Where(x => x.ApprovedPaymentDate <= expencesViewModel.ToFilter ||
                                                           x.DesiredPaymentDate <= expencesViewModel.ToFilter);
            if (!expencesViewModel.AdmFilter) defaultContext = defaultContext.Where(x => x.Person.UserId == currentUserId);

            expencesViewModel.Expences = await PaginatedList<Expences>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize);
            expencesViewModel.Total = total;
            return expencesViewModel;
        }
        public SelectList GetCurrenciesIds()
        {
            return new SelectList(_context.Currency.Where(x => x.Active), "Id", "Name");
        }
        public SelectList GetCompaniesIds()
        {
            return new SelectList(_context.Expences.Select(x => x.Company).Distinct(), "Id", "Name");
        }
        public SelectList GetOurCompaniesIds()
        {
            return new SelectList(_context.Company.Where(x => x.OurCompany), "Id", "Name");
        }
        public SelectList GetDocumentTypesIds()
        {
            return new SelectList(_context.DocumentType, "Id", "Name");
        }
        public SelectList GetPersonsIds(string currentUserId)
        {
            return new SelectList(_context.Person.Where(x => x.UserId == currentUserId && x.UserId != null), "Id", "Name");
        }
        public SelectList GetUsersIds(string currentUserId)
        {
            return new SelectList(_context.Users.Where(x => x.Id == currentUserId), "Id", "Email");
        }
    }
}
