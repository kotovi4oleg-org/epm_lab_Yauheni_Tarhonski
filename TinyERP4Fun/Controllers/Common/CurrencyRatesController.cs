using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    public class CurrencyRatesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly ICommonService _commonService;

        public CurrencyRatesController(DefaultContext context, ICommonService commonService)
        {
            _context = context;
            _commonService = commonService;
        }

         [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> UpdateBYN()
        {
            await _commonService.UpdateBYNVoid();
            return RedirectToAction(nameof(Index));

        }

        // GET: CurrencyRates
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var defaultContext = _context.CurrencyRates.Include(c => c.BaseCurrency)
                                                       .Include(c => c.Currency)
                                                       .OrderByDescending(x => x.DateRate)
                                                       .ThenBy(x => x.Currency.Name);
            return View(await PaginatedList<CurrencyRates>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: CurrencyRates/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var currencyRates = await _commonService.GetCurrencyRatesInfo(id);
            if (currencyRates == null) return NotFound();
            return View(currencyRates);
        }
        private void SetViewData()
        {
            ViewData["BaseCurrencyId"] = new SelectList(_context.Currency, "Id", "Code");
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code");
        }
        // GET: CurrencyRates/Create
        [Authorize(Roles = Constants.adminRoleName)]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // POST: CurrencyRates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CurrecyScale,CurrencyRate,DateRate,CurrencyId,BaseCurrencyId")] CurrencyRates currencyRates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currencyRates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(currencyRates);
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // GET: CurrencyRates/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var currencyRates = await _commonService.GetCurrencyRatesInfo(id);
            if (currencyRates == null) return NotFound();
            SetViewData();
            return View(currencyRates);
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // POST: CurrencyRates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CurrecyScale,CurrencyRate,DateRate,CurrencyId,BaseCurrencyId")] CurrencyRates currencyRates)
        {
            if (id != currencyRates.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currencyRates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyRatesExists(currencyRates.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(currencyRates);
        }
        // GET: CurrencyRates/Delete/5
        [Authorize(Roles = Constants.adminRoleName)]
        public async Task<IActionResult> Delete(long? id)
        {
            var currencyRates = await _commonService.GetCurrencyRatesInfo(id);
            if (currencyRates == null) return NotFound();
            return View(currencyRates);
        }
        // POST: CurrencyRates/Delete/5
        [Authorize(Roles = Constants.adminRoleName)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var currencyRates = await _context.CurrencyRates.FindAsync(id);
            _context.CurrencyRates.Remove(currencyRates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CurrencyRatesExists(long id)
        {
            return _context.CurrencyRates.Any(e => e.Id == id);
        }
    }
}
