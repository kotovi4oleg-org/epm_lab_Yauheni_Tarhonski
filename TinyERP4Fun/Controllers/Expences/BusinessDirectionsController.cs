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
using TinyERP4Fun.Models.Expenses;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class BusinessDirectionsController : Controller
    {
        private readonly DefaultContext _context;

        public BusinessDirectionsController(DefaultContext context)
        {
            _context = context;
        }

        // GET: BusinessDirections
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusinessDirection.ToListAsync());
        }

        // GET: BusinessDirections/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessDirection = await _context.BusinessDirection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (businessDirection == null)
            {
                return NotFound();
            }

            return View(businessDirection);
        }

        // GET: BusinessDirections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusinessDirections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BusinessDirection businessDirection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessDirection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(businessDirection);
        }

        // GET: BusinessDirections/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessDirection = await _context.BusinessDirection.FindAsync(id);
            if (businessDirection == null)
            {
                return NotFound();
            }
            return View(businessDirection);
        }

        // POST: BusinessDirections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] BusinessDirection businessDirection)
        {
            if (id != businessDirection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessDirection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessDirectionExists(businessDirection.Id))
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
            return View(businessDirection);
        }

        // GET: BusinessDirections/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessDirection = await _context.BusinessDirection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (businessDirection == null)
            {
                return NotFound();
            }

            return View(businessDirection);
        }

        // POST: BusinessDirections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var businessDirection = await _context.BusinessDirection.FindAsync(id);
            _context.BusinessDirection.Remove(businessDirection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessDirectionExists(long? id)
        {
            return _context.BusinessDirection.Any(e => e.Id == id);
        }
    }
}
