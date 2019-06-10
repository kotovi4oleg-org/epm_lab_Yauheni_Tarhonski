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
    public class CompaniesController : Controller
    {
        private readonly DefaultContext _context;

        public CompaniesController(DefaultContext context)
        {
            _context = context;
        }
        public ActionResult GetCities(int id)
        {
            return PartialView(_context.City.Where(x => x.StateId == id).ToList());
        }
        public ActionResult GetStates(int id)
        {
            return PartialView(_context.State.Where(x => x.CountryId == id).ToList());
        }
        // GET: Companies
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CitySortParm"] = sortOrder == "city" ? "city_desc" : "city";
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



            IQueryable<Company> result = _context.Company
                                                 .Include(x => x.City)
                                                 .Include(x => x.City.State)
                                                 .Include(x => x.City.State.Country);

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.Name.Contains(searchString)
                                       || x.City.Name.Contains(searchString)
                                       || x.City.State.Name.Contains(searchString)
                                       || x.City.State.Country.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(x => x.Name);
                    break;
                case "city":
                    result = result.OrderBy(x => x.City.Name).ThenBy(x => x.Name);
                    break;
                case "city_desc":
                    result = result.OrderByDescending(x => x.City.Name).ThenBy(x => x.Name);
                    break;
                case "state":
                    result = result.OrderBy(x => x.City.State.Name).ThenBy(x => x.Name);
                    break;
                case "state_desc":
                    result = result.OrderByDescending(x => x.City.State.Name).ThenBy(x => x.Name);
                    break;
                case "country":
                    result = result.OrderBy(x => x.City.State.Country.Name).ThenBy(x => x.Name);
                    break;
                case "country_desc":
                    result = result.OrderByDescending(x => x.City.State.Country.Name).ThenBy(x => x.Name);
                    break;
                default:
                    result = result.OrderBy(x => x.Name);
                    break;
            }

            return View(await PaginatedList<Company>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                                        .Include(x => x.City)
                                        .Include(x => x.City.State)
                                        .Include(x => x.City.State.Country)
                                        .Include(x => x.HeadCompany)
                                        .Include(x => x.BaseCurrency)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            ViewBag.Cities = null;
            ViewBag.States = null;
            ViewBag.Currencies = CommonFunctions.AddFirstItem(new SelectList(_context.Currency, "Id", "Name"));
            ViewBag.Companies = CommonFunctions.AddFirstItem(new SelectList(_context.Company, "Id", "Name"));
            ViewBag.Countries = CommonFunctions.AddFirstItem(new SelectList(_context.Country, "Id", "Name"));
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TIN,TIN2,Address,Address2,OurCompany,CityId,HeadCompanyId,BaseCurrencyId")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company.Include(x => x.City)
                                                .Include(x => x.City.State)
                                                .Include(x => x.City.State.Country)
                                                .Include(x => x.City.State.Country)
                                                .Include(x => x.HeadCompany)
                                                .Include(x => x.BaseCurrency)
                                                .FirstOrDefaultAsync(x=>x.Id == id);
            if (company == null)
                return NotFound();

            if (company.CityId == null)
            {
                ViewBag.Cities = null;
                ViewBag.States = null;
            }
            else
            {
                ViewBag.Cities = CommonFunctions.AddFirstItem(new SelectList(_context.City.Where(x => x.StateId == company.City.StateId), "Id", "Name"));
                ViewBag.States = CommonFunctions.AddFirstItem(new SelectList(_context.State.Where(x => x.CountryId == company.City.State.CountryId), "Id", "Name"));
            }
            ViewBag.Currencies = CommonFunctions.AddFirstItem(new SelectList(_context.Currency, "Id", "Name"));
            ViewBag.Companies = CommonFunctions.AddFirstItem(new SelectList(_context.Company, "Id", "Name"));
            ViewBag.Countries = CommonFunctions.AddFirstItem(new SelectList(_context.Country, "Id", "Name"));
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,TIN,TIN2,Address,Address2,OurCompany,CityId,HeadCompanyId,BaseCurrencyId")] Company company)
        {
            if (id != company.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(company);
            try
            {
                _context.Update(company);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(company.Id))
                {
                    return NotFound();
                }
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var company = await _context.Company.FindAsync(id);
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(long? id)
        {
            return _context.Company.Any(e => e.Id == id);
        }
    }
}
