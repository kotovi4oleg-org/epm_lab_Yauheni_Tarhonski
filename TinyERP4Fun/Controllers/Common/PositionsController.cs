using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class PositionsController : Controller
    {
        private readonly IPositionsService _positionsService;

        public PositionsController(IPositionsService positionsService)
        {
            _positionsService = positionsService;
        }


        // GET: Positions
        public async Task<IActionResult> Index()
        {
            return View(await _positionsService.GetListAsync());
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _positionsService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Position position)
        {
            if (ModelState.IsValid)
            {
                await _positionsService.AddAsync(position);
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _positionsService.GetAsync(id, true);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Positions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Position position)
        {
            if (id != position.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!await _positionsService.UpdateAsync(position)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _positionsService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _positionsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
