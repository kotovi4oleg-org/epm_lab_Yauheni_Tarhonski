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
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class CitiesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly ICommonService _commonService;

        public CitiesController(DefaultContext context, ICommonService commonService)
        {
            _context = context;
            _commonService = commonService;
        }

        // GET: Cities
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StateSortParm"] = sortOrder == "state" ? "state_desc" : "state";
            ViewData["CountrySortParm"] = sortOrder == "country" ? "country_desc" : "country";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null) pageNumber = 1;
            else searchString = currentFilter;

            IQueryable<City> result = _commonService.GetFiltredCities(sortOrder, searchString);
            return View(await PaginatedList<City>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var cityInfo = await _commonService.GetCityInfo(id);
            if (cityInfo == null) return NotFound();
            return View(cityInfo);
        }
        private void SetViewBag (City city)
        {
            if (city == null || city.State == null)
                ViewBag.States = null;
            else
                ViewBag.States = ControllerCommonFunctions.AddFirstItem(new SelectList(_context.State.Where(x => x.CountryId == city.State.CountryId), "Id", "Name"));
            ViewBag.Countries = ControllerCommonFunctions.AddFirstItem(new SelectList(_context.Country, "Id", "Name"));
        }
        // GET: Cities/Create
        public IActionResult Create()
        {
            SetViewBag(null);
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StateId")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetViewBag(city);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var city = await _commonService.GetCityInfo(id);
            if (city == null) return NotFound();
            SetViewBag(city);
            return View(city);
        }

        public ActionResult GetStates(int id)
        {
            return PartialView(_context.State.Where(x => x.CountryId == id).ToList());
        }

        // POST: Cities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,StateId")] City city)
        {
            if (id != city.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            SetViewBag(city);
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var cityInfo = await _commonService.GetCityInfo(id);
            if (cityInfo == null) return NotFound();
            return View(cityInfo);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var city = await _context.City.FindAsync(id);
            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(long? id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}
