using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers.Common
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class CurrenciesController : Controller
    {
        private readonly ICurrenciesService _currenciesService;

        public CurrenciesController(ICurrenciesService currenciesService)
        {
            _currenciesService = currenciesService;
        }

        // GET: Currencies
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<Currency>.CreateAsync(_currenciesService.GetIQueryable(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _currenciesService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Name2,Part001Name,Part001Name2,Active,Base")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                await _currenciesService.AddAsync(currency);
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _currenciesService.GetAsync(id, true);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Currencies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,Name,Name2,Part001Name,Part001Name2,Active,Base")] Currency currency)
        {
            if (id != currency.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!await _currenciesService.UpdateAsync(currency)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _currenciesService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _currenciesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
