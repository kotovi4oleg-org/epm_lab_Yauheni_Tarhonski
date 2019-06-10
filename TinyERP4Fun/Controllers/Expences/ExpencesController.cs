using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ViewModels;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesExpences_User)]
    public class ExpencesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExpencesController(DefaultContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Expences

        private async Task<ExpencesViewModel> IndexCreateViewModel(int? pageNumber,
                                               IEnumerable<long?> currencyFilter,
                                               IEnumerable<long?> companyFilter,
                                               IEnumerable<long?> ourcompanyFilter,
                                               DateTime? fromFilter,
                                               DateTime? toFilter,
                                               bool approvedFilter,
                                               bool declinedFilter,
                                               bool notProcessedFilter,
                                               bool adm
                                               )
        {
            IQueryable<Expences> defaultContext = _context.Expences.Include(e => e.DocumentType)
                                                                   .Include(e => e.Company)
                                                                   .Include(e => e.Currency)
                                                                   .Include(e => e.OurCompany)
                                                                   .Include(e => e.Person)
                                                                   .Include(e => e.User);
            string total = null;
            var currentUserId = _userManager.GetUserId(User);

            if (currencyFilter.Count() != 0)
            {
                defaultContext = defaultContext.Where(x => currencyFilter.Contains(x.CurrencyId));
                if(currencyFilter.Count()==1)
                        total = Localization.currentLocalizatin["Amount of Expenses"] + ": " + defaultContext.Sum(x => x.AmountOfPayment).ToString();
            }
            if (companyFilter.Count() != 0) defaultContext = defaultContext.Where(x => companyFilter.Contains(x.CompanyId));
            if (ourcompanyFilter.Count() != 0) defaultContext = defaultContext.Where(x => ourcompanyFilter.Contains(x.OurCompanyId));
            if (fromFilter != null) defaultContext = defaultContext.Where(x => (x.ApprovedPaymentDate != null && x.ApprovedPaymentDate >= fromFilter) ||
                                                                               (x.ApprovedPaymentDate == null && x.DesiredPaymentDate >= fromFilter));
            if (toFilter != null) defaultContext = defaultContext.Where(x => (x.ApprovedPaymentDate != null && x.ApprovedPaymentDate <= toFilter) ||
                                                                             (x.ApprovedPaymentDate == null && x.DesiredPaymentDate <= toFilter));
            if (!adm) 
                defaultContext = defaultContext.Where(x=>x.Person.UserId == currentUserId);

            ExpencesViewModel expencesViewModel = new ExpencesViewModel()
            {
                Expences = await PaginatedList<Expences>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize),
                CurrencyFilter = new SelectList(_context.Currency.Where(x => x.Active), "Id", "Name"),
                CompanyFilter = new SelectList(_context.Company, "Id", "Name"),
                OurCompanyFilter = new SelectList(_context.Company.Where(x => x.OurCompany), "Id", "Name"),
                FromFilter = fromFilter,
                ToFilter = toFilter,
                ApprovedFilter = approvedFilter,
                DeclinedFilter = declinedFilter,
                NotProcessedFilter = notProcessedFilter,
                Total = total
            };

            return expencesViewModel;
        }

        public async Task<IActionResult> Index(int? pageNumber,
                                               IEnumerable<long?> currencyFilter,
                                               IEnumerable<long?> companyFilter,
                                               IEnumerable<long?> ourcompanyFilter,
                                               DateTime? fromFilter, 
                                               DateTime? toFilter, 
                                               bool approvedFilter, 
                                               bool declinedFilter, 
                                               bool notProcessedFilter
                                               )
        {
             return View(await IndexCreateViewModel(pageNumber, currencyFilter, companyFilter, ourcompanyFilter, fromFilter,
                   toFilter, approvedFilter, declinedFilter, notProcessedFilter, false));
        }
        [Authorize(Roles = Constants.rolesExpences_Admin)]
        public async Task<IActionResult> IndexAdm(int? pageNumber,
                                       IEnumerable<long?> currencyFilter,
                                       IEnumerable<long?> companyFilter,
                                       IEnumerable<long?> ourcompanyFilter,
                                       DateTime? fromFilter,
                                       DateTime? toFilter,
                                       bool approvedFilter,
                                       bool declinedFilter,
                                       bool notProcessedFilter
                                       )
        {
            return View(await IndexCreateViewModel(pageNumber, currencyFilter, companyFilter, ourcompanyFilter, fromFilter,
                               toFilter, approvedFilter, declinedFilter, notProcessedFilter, true));
        }

        // GET: Expences/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expences = await _context.Expences.Include(e => e.DocumentType)
                                                  .Include(e => e.Company)
                                                  .Include(e => e.Currency)
                                                  .Include(e => e.OurCompany)
                                                  .Include(e => e.Person)
                                                  .Include(e => e.User)
                                                  .FirstOrDefaultAsync(m => m.Id == id);
            if (expences == null)
            {
                return NotFound();
            }

            return View(expences);
        }

        // GET: Expences/Create
        public IActionResult Create()
        {
            //Получили Id usera
            var currentUserId= _userManager.GetUserId(User);
            ViewData["DocumentTypeId"] = new SelectList(_context.DocumentType, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currency.Where(x => x.Active), "Id", "Name");
            ViewData["OurCompanyId"] = new SelectList(_context.Company.Where(x => x.OurCompany), "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person.Where(x=>x.UserId == currentUserId&& x.UserId!=null) , "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(x=>x.Id==currentUserId), "Id", "Email");
            return View();
        }

        // POST: Expences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocumentNumber,DocumentDate,DocumentTypeId,PersonId,UserId,OurCompanyId,CompanyId,DesiredPaymentDate,ApprovedPaymentDate,AmountOfPayment,CurrencyId,Approved,Declined,PurposeOfPayment")] Expences expences)
        {
            if (ModelState.IsValid)
            {
                #region можно улучшить. но не буду (предпочту лишний раз обратиться в базу, возможно зря)
                // Получаем user ID не делая дополнительную вылазку в базу
                /*
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = claim.Value;
                 
                // Но можно выкинуть этот код в отдельный класс
                
                public static class UserHelpers
                {
                    public static string GetUserId(this IPrincipal principal)
                    {
                        var claimsIdentity = (ClaimsIdentity)principal.Identity;
                        var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                        return claim?.Value;
                    }
                }
                
                // И обращаться так: var userId = this.User.GetUserId();
                /**/
                #endregion

                //Повторно считали Id пользователя 
                expences.UserId = _userManager.GetUserId(User);

                _context.Add(expences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(expences);
        }

        // GET: Expences/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return NotFound();

            var expences = await _context.Expences.FindAsync(id);
            if (expences == null)
                return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            ViewData["DocumentTypeId"] = new SelectList(_context.DocumentType, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currency.Where(x => x.Active), "Id", "Name");
            ViewData["OurCompanyId"] = new SelectList(_context.Company.Where(x => x.OurCompany), "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person.Where(x => x.UserId == currentUserId && x.UserId != null), "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(x => x.Id == currentUserId), "Id", "Email");
            return View(expences);
        }

        // POST: Expences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DocumentNumber,DocumentDate,DocumentTypeId,PersonId,UserId,OurCompanyId,CompanyId,DesiredPaymentDate,AmountOfPayment,CurrencyId,PurposeOfPayment")] Expences expences)
        {
            if (id != expences.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpencesExists(expences.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expences);
        }
        [Authorize(Roles = Constants.rolesExpences_Admin)]
        public async Task<IActionResult> EditAdm(long? id)
        {
            if (id == null)
                return NotFound();

            var expences = await _context.Expences.FindAsync(id);
            if (expences == null)
                return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            ViewData["DocumentTypeId"] = new SelectList(_context.DocumentType, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currency.Where(x => x.Active), "Id", "Name");
            ViewData["OurCompanyId"] = new SelectList(_context.Company.Where(x => x.OurCompany), "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person.Where(x => x.Id == expences.PersonId), "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(x => x.Id == currentUserId), "Id", "Email");
            return View(expences);
        }

        // POST: Expences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.rolesExpences_Admin)]
        public async Task<IActionResult> EditAdm(long id, [Bind("Id,DocumentNumber,DocumentDate,DocumentTypeId,PersonId,UserId,OurCompanyId,CompanyId,DesiredPaymentDate,ApprovedPaymentDate,AmountOfPayment,CurrencyId,Approved,Declined,PurposeOfPayment")] Expences expences)
        {
            if (id != expences.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpencesExists(expences.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexAdm));
            }
            return View(expences);
        }

        public async Task<Expences> DeleteCommon(long? id)
        {
            if (id == null) return null;
            var expences = await _context.Expences.Include(e => e.DocumentType)
                                                  .Include(e => e.Company)
                                                  .Include(e => e.Currency)
                                                  .Include(e => e.OurCompany)
                                                  .Include(e => e.Person)
                                                  .Include(e => e.User)
                                                  .FirstOrDefaultAsync(m => m.Id == id);

            return expences;
        }

        private async Task DeleteConfirmedCommon(long id)
        {
            var expences = await _context.Expences.FindAsync(id);
            _context.Expences.Remove(expences);
            await _context.SaveChangesAsync();
        }
        // GET: Expences/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var expences = await DeleteCommon(id);
            if (expences == null) return NotFound();
            return View(expences);
        }

        // POST: Expences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await DeleteConfirmedCommon(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Expences/Delete/5
        [Authorize(Roles = Constants.rolesExpences_Admin)]
        public async Task<IActionResult> DeleteAdm(long? id)
        {
            var expences = await DeleteCommon(id);
            if (expences == null) return NotFound();
            return View(expences);
        }

        // POST: Expences/Delete/5
        [HttpPost, ActionName("DeleteAdm")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.rolesExpences_Admin)]
        public async Task<IActionResult> DeleteAdmConfirmed(long id)
        {
            await DeleteConfirmedCommon(id);
            return RedirectToAction(nameof(IndexAdm));
        }

        private bool ExpencesExists(long id)
        {
            return _context.Expences.Any(e => e.Id == id);
        }
    }
}
