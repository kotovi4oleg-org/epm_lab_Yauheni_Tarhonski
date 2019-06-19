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
        
        private readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }
        public ActionResult GetCities(long stateId)
        {
            return PartialView(_companiesService.GetCities(stateId));
        }
        public ActionResult GetStates(long countryId)
        {
            return PartialView(_companiesService.GetCities(countryId));
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

            IQueryable<Company> result = _companiesService.GetFiltredContent(sortOrder, searchString);
            return View(await PaginatedList<Company>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var company = await _companiesService.GetAsync(id);
            if (company == null) return NotFound();
            return View(company);
        }

        private void SetViewBag(Company company)
        {
            if (company==null||company.CityId == null)
                ViewBag.Cities = ViewBag.States = null;
            else
            {
                ViewBag.Cities = _companiesService.GetCitiesIds(company.City.StateId);
                ViewBag.States = _companiesService.GetStatesIds(company.City.State.CountryId);
            }
            ViewBag.Currencies = _companiesService.GetCurrenciesIds();
            ViewBag.Companies = _companiesService.GetCompaniesIds();
            ViewBag.Countries = _companiesService.GetCountriesIds();
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
                await _companiesService.AddAsync(company);
                return RedirectToAction(nameof(Index));
            }
            SetViewBag(company);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var company = await _companiesService.GetAsync(id, true);
            if (company == null) return NotFound();
            SetViewBag(company);
            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,TIN,TIN2,Address,Address2,OurCompany,CityId,HeadCompanyId,BaseCurrencyId")] Company company)
        {
            if (id != company.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if(!await _companiesService.UpdateAsync(company)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewBag(company);
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var company = await _companiesService.GetAsync(id);
            if (company == null) return NotFound();
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _companiesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
