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
        private readonly ICurrencyRatesService _currencyRatesService;

        public CurrencyRatesController(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }

         [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> UpdateBYN()
        {
            await _currencyRatesService.UpdateBYNVoid();
            return RedirectToAction(nameof(Index));
        }

        // GET: CurrencyRates
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<CurrencyRates>.CreateAsync(_currencyRatesService.GetIQueryable(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: CurrencyRates/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var currencyRates = await _currencyRatesService.GetAsync(id);
            if (currencyRates == null) return NotFound();
            return View(currencyRates);
        }
        private void SetViewData()
        {
            ViewData["BaseCurrencyId"] = _currencyRatesService.GetCurrenciesIds();
            ViewData["CurrencyId"] = _currencyRatesService.GetCurrenciesIds();
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
                await _currencyRatesService.AddAsync(currencyRates);
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(currencyRates);
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // GET: CurrencyRates/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var currencyRates = await _currencyRatesService.GetAsync(id, true);
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
                if (! await _currencyRatesService.UpdateAsync(currencyRates)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(currencyRates);
        }
        // GET: CurrencyRates/Delete/5
        [Authorize(Roles = Constants.adminRoleName)]
        public async Task<IActionResult> Delete(long? id)
        {
            var currencyRates = await _currencyRatesService.GetAsync(id);
            if (currencyRates == null) return NotFound();
            return View(currencyRates);
        }
        // POST: CurrencyRates/Delete/5
        [Authorize(Roles = Constants.adminRoleName)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _currencyRatesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
