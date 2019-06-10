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
    public class CostItemsController : Controller
    {
        private readonly DefaultContext _context;

        public CostItemsController(DefaultContext context)
        {
            _context = context;
        }

        // GET: CostItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.CostItem.ToListAsync());
        }

        // GET: CostItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costItem = await _context.CostItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costItem == null)
            {
                return NotFound();
            }

            return View(costItem);
        }

        // GET: CostItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CostItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CostItem costItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(costItem);
        }

        // GET: CostItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costItem = await _context.CostItem.FindAsync(id);
            if (costItem == null)
            {
                return NotFound();
            }
            return View(costItem);
        }

        // POST: CostItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] CostItem costItem)
        {
            if (id != costItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostItemExists(costItem.Id))
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
            return View(costItem);
        }

        // GET: CostItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costItem = await _context.CostItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costItem == null)
            {
                return NotFound();
            }

            return View(costItem);
        }

        // POST: CostItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var costItem = await _context.CostItem.FindAsync(id);
            _context.CostItem.Remove(costItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostItemExists(long? id)
        {
            return _context.CostItem.Any(e => e.Id == id);
        }
    }
}
