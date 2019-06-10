using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class CitiesController : Controller
    {
        private readonly DefaultContext _context;

        public CitiesController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StateSortParm"] = sortOrder == "state" ? "state_desc" : "state";
            ViewData["CountrySortParm"] = sortOrder == "country" ? "country_desc" : "country";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<City> result = _context.City.Include(x => x.State).Include(x => x.State.Country);

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.Name.Contains(searchString)
                                       || x.State.Name.Contains(searchString)
                                       || x.State.Country.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(x => x.Name);
                    break;
                case "state":
                    result = result.OrderBy(x => x.State.Name).ThenBy(x => x.Name);
                    break;
                case "state_desc":
                    result = result.OrderByDescending(x => x.State.Name).ThenBy(x => x.Name);
                    break;
                case "country":
                    result = result.OrderBy(x => x.State.Country.Name).ThenBy(x => x.Name);
                    break;
                case "country_desc":
                    result = result.OrderByDescending(x => x.State.Country.Name).ThenBy(x => x.Name);
                    break;
                default:
                    result = result.OrderBy(x => x.Name);
                    break;
            }
            return View(await PaginatedList<City>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }
        private async Task<City> CityInfo(long? id)
        {
            if (id == null) return null;
            var city = await _context.City.Include(x => x.State)
                              .Include(x => x.State.Country)
                              .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null) return null;
            return city;
        }
        // GET: Cities/Details/5
        public async Task<IActionResult> Details(long? id)
        {

            var cityInfo = await CityInfo(id);
            if (cityInfo == null)
                return NotFound();
            else
                return View(cityInfo);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewBag.States = null;
            ViewBag.Countries = CommonFunctions.AddFirstItem(new SelectList(_context.Country, "Id", "Name"));
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
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City.Include(x => x.State)
                                          .Include(x => x.State.Country)
                                          .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            ViewBag.States = CommonFunctions.AddFirstItem(new SelectList(_context.State.Where(x=>x.CountryId == city.State.CountryId), "Id", "Name"));
            ViewBag.Countries = CommonFunctions.AddFirstItem(new SelectList(_context.Country, "Id", "Name"));
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
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
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
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City.Include(x => x.State)
                                          .Include(x => x.State.Country)
                                          .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
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
