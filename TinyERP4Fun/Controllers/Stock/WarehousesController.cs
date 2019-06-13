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
    public class WarehousesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IGeneralService _generalService;

        public WarehousesController(DefaultContext context, IGeneralService generalService)
        {
            _context = context;
            _generalService = generalService;
        }

        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Warehouse.ToListAsync());
        }

        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _generalService.GetObject<Warehouse>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: Warehouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _generalService.GetObject<Warehouse>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Warehouses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Warehouse warehouse)
        {
            if (id != warehouse.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(warehouse.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _generalService.GetObject<Warehouse>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var warehouse = await _context.Warehouse.FindAsync(id);
            _context.Warehouse.Remove(warehouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseExists(long id)
        {
            return _context.Warehouse.Any(e => e.Id == id);
        }
    }
}
