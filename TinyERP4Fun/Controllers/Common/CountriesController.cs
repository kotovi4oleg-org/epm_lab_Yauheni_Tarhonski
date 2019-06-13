using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class CountriesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IGeneralService _generalService;

        public CountriesController(DefaultContext context, IGeneralService generalService)
        {
            _context = context;
            _generalService = generalService;
        }

        // GET: Countries
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var defaultContext = _context.Country.OrderBy(x => x.Name);
            return View(await PaginatedList<Country>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _generalService.GetObject<Country>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _generalService.GetObject<Country>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _generalService.GetObject<Country>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var country = await _context.Country.FindAsync(id);
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(long? id)
        {
            return _context.Country.Any(e => e.Id == id);
        }
    }
}
