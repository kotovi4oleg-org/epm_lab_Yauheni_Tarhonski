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
    public class ExpencesService: IExpencesService
    {
        private readonly DefaultContext _context;
        public ExpencesService(DefaultContext context)
        {
            _context = context;
        }
        public async Task<Expences> GetExpenceInfo(long? id)
        {
            if (id == null) return null;
            var resultObject = await _context.Expences.Include(e => e.DocumentType)
                                                      .Include(e => e.Company)
                                                      .Include(e => e.Currency)
                                                      .Include(e => e.OurCompany)
                                                      .Include(e => e.Person)
                                                      .Include(e => e.User)
                                                      .SingleOrDefaultAsync(m => m.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }

        public async Task<ExpencesViewModel> GetFilteredExpences(int? pageNumber, ExpencesViewModel expencesViewModel, string currentUserId, bool adm)
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
                if (currencyFilter != null && currencyFilter.Count() != 0)
                {
                    defaultContext = defaultContext.Where(x => currencyFilter.Contains(x.CurrencyId));
                    if (currencyFilter.Count() == 1)
                        total = Localization.currentLocalizatin["Amount of Expenses"] + ": " + defaultContext.Sum(x => x.AmountOfPayment).ToString();
                }
            }

            if (expencesViewModel.CompanyFilter != null && expencesViewModel.CompanyFilter.Count() != 0)
                defaultContext = defaultContext.Where(x => expencesViewModel.CompanyFilter.Contains(x.CompanyId));
            if (expencesViewModel.OurCompanyFilter != null && expencesViewModel.OurCompanyFilter.Count() != 0)
                defaultContext = defaultContext.Where(x => expencesViewModel.OurCompanyFilter.Contains(x.OurCompanyId));

            if (expencesViewModel.FromFilter != null)
                defaultContext = defaultContext.Where(x => (x.ApprovedPaymentDate != null && x.ApprovedPaymentDate >= expencesViewModel.FromFilter) ||
                                                                               (x.ApprovedPaymentDate == null && x.DesiredPaymentDate >= expencesViewModel.FromFilter));
            if (expencesViewModel.ToFilter != null)
                defaultContext = defaultContext.Where(x => (x.ApprovedPaymentDate != null && x.ApprovedPaymentDate <= expencesViewModel.ToFilter) ||
                                                           (x.ApprovedPaymentDate == null && x.DesiredPaymentDate <= expencesViewModel.ToFilter));
            if (!expencesViewModel.AdmFilter) defaultContext = defaultContext.Where(x => x.Person.UserId == currentUserId);

            expencesViewModel.Expences = await PaginatedList<Expences>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize);

            return expencesViewModel;
        }
    }
}
