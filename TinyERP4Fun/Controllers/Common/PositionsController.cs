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
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class PositionsController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IGeneralService _generalService;

        public PositionsController(DefaultContext context, IGeneralService generalService)
        {
            _context = context;
            _generalService = generalService;
        }


        // GET: Positions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Position.ToListAsync());
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _generalService.GetObject<Position>(id);
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
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _generalService.GetObject<Position>(id);
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
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _generalService.GetObject<Position>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var position = await _context.Position.FindAsync(id);
            _context.Position.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(long? id)
        {
            return _context.Position.Any(e => e.Id == id);
        }
    }
}
