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
    public class CompaniesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly ICommonService _commonService;

        public CompaniesController(DefaultContext context, ICommonService commonService)
        {
            _context = context;
            _commonService = commonService;
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

            if (searchString != null) pageNumber = 1;
            else searchString = currentFilter;

            IQueryable<Company> result = _commonService.GetFiltredCompanies(sortOrder, searchString);
            return View(await PaginatedList<Company>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var company = await _commonService.GetCompanyInfo(id);
            if (company == null) return NotFound();
            return View(company);
        }

        private void SetViewBag(Company company)
        {
            if (company==null||company.CityId == null)
            {
                ViewBag.Cities = ViewBag.States = null;
            }
            else
            {
                ViewBag.Cities = CommonFunctions.AddFirstItem(new SelectList(_context.City.Where(x => x.StateId == company.City.StateId), "Id", "Name"));
                ViewBag.States = CommonFunctions.AddFirstItem(new SelectList(_context.State.Where(x => x.CountryId == company.City.State.CountryId), "Id", "Name"));
            }
            ViewBag.Currencies = CommonFunctions.AddFirstItem(new SelectList(_context.Currency, "Id", "Name"));
            ViewBag.Companies = CommonFunctions.AddFirstItem(new SelectList(_context.Company, "Id", "Name"));
            ViewBag.Countries = CommonFunctions.AddFirstItem(new SelectList(_context.Country, "Id", "Name"));
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            SetViewBag(null);
            return View();
        }

        // POST: Companies/Create
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
            SetViewBag(company);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var company = await _commonService.GetCompanyInfo(id);
            if (company == null) return NotFound();
            SetViewBag(company);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,TIN,TIN2,Address,Address2,OurCompany,CityId,HeadCompanyId,BaseCurrencyId")] Company company)
        {
            if (id != company.Id) return NotFound();

            if (ModelState.IsValid) //return View(company)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            SetViewBag(company);
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var company = await _commonService.GetCompanyInfo(id);
            if (company == null) return NotFound();
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
