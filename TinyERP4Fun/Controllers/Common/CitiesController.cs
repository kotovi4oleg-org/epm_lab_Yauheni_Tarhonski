﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class CitiesController : Controller
    {
        private readonly ICitiesService _citiesService;
        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
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

            IQueryable<City> result = _citiesService.GetFiltredCities(sortOrder, searchString);
            return View(await PaginatedList<City>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var cityInfo = await _citiesService.GetAsync(id);
            if (cityInfo == null) return NotFound();
            return View(cityInfo);
        }
        private void SetViewBag (City city)
        {
            if (city == null || city.State == null)
                ViewBag.States = null;
            else
                ViewBag.States = ControllerCommonFunctions.AddFirstItem(
                    new SelectList(_citiesService.GetStatesIds(city.State.CountryId),"Id","Name"));
            ViewBag.Countries = ControllerCommonFunctions.AddFirstItem(
                    new SelectList(_citiesService.GetCountriesIds(), "Id", "Name"));
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
                await _citiesService.AddAsync(city);
                return RedirectToAction(nameof(Index));
            }
            SetViewBag(city);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var city = await _citiesService.GetAsync(id,true);
            if (city == null) return NotFound();
            SetViewBag(city);
            return View(city);
        }

        public ActionResult GetStates(long id)
        {
            return PartialView(_citiesService.GetStates(id));
        }

        // POST: Cities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,StateId")] City city)
        {
            if (id != city.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if(!await _citiesService.UpdateAsync(city)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewBag(city);
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var cityInfo = await _citiesService.GetAsync(id);
            if (cityInfo == null) return NotFound();
            return View(cityInfo);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _citiesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
