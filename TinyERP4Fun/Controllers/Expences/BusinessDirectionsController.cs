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
using TinyERP4Fun.ModelServiceInterfaces;
using TinyERP4Fun.ModelServises;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class BusinessDirectionsController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IGeneralService _generalService;

        public BusinessDirectionsController(DefaultContext context, IGeneralService generalService)
        {
            _context = context;
            _generalService = generalService;
        }

        // GET: BusinessDirections
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusinessDirection.ToListAsync());
        }

        // GET: BusinessDirections/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _generalService.GetObject<BusinessDirection>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: BusinessDirections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusinessDirections/Create
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
            var result = await _generalService.GetObject<BusinessDirection>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: BusinessDirections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] BusinessDirection businessDirection)
        {
            if (id != businessDirection.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessDirection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessDirectionExists(businessDirection.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(businessDirection);
        }

        // GET: BusinessDirections/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _generalService.GetObject<BusinessDirection>(id);
            if (result == null) return NotFound();
            return View(result);
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
