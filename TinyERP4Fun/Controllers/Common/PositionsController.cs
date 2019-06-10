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

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class PositionsController : Controller
    {
        private readonly DefaultContext _context;

        public PositionsController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Position.ToListAsync());
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Position
                .FirstOrDefaultAsync(m => m.Id == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Position.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Position position)
        {
            if (id != position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.Id))
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
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Position
                .FirstOrDefaultAsync(m => m.Id == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
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
