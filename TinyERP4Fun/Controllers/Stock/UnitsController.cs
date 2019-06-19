using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    public class UnitsController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        // GET: Units
        public async Task<IActionResult> Index()
        {
            return View(await _unitService.GetListAsync());
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _unitService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")]Unit unit)
        {
            if (ModelState.IsValid)
            {
                await _unitService.AddAsync(unit);
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: Units/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _unitService.GetAsync(id, true);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Units/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Unit unit)
        {
            if (id != unit.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!await _unitService.UpdateAsync(unit)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: Units/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _unitService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _unitService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
