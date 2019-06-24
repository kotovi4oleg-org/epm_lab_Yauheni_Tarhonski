using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class CountriesController : Controller
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        // GET: Countries
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<Country>.CreateAsync(_countriesService.GetIQueryable(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _countriesService.GetAsync(id);
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
                await _countriesService.AddAsync(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _countriesService.GetAsync(id, true);
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
                if(!await _countriesService.UpdateAsync(country)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _countriesService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _countriesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
