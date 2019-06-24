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
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class StatesController : Controller
    {
        private readonly IStatesService _statesService;

        public StatesController(IStatesService statesService)
        {
            _statesService = statesService;
        }

        // GET: States
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<State>.CreateAsync(_statesService.GetIQueryable(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var state = await _statesService.GetAsync(id);
            if (state == null) return NotFound();
            return View(state);
        }
        public void SetViewBag()
        {
            ViewBag.Countries = ControllerCommonFunctions.AddFirstItem(
                new SelectList(_statesService.GetCountriesIds(), "Id", "Name"));
        }
        // GET: States/Create
        public IActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: States/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CountryId")] State state)
        {
            if (ModelState.IsValid)
            {
                await _statesService.AddAsync(state);
                return RedirectToAction(nameof(Index));
            }
            SetViewBag();
            return View(state);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var state = await _statesService.GetAsync(id, true);
            if (state == null) return NotFound();
            SetViewBag();
            return View(state);
        }

        // POST: States/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] State state)
        {
            if (id != state.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!await _statesService.UpdateAsync(state)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewBag();
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var state = await _statesService.GetAsync(id);
            if (state == null) return NotFound();
            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _statesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
