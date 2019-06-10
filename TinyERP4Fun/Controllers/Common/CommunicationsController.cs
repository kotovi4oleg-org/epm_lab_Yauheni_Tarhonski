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
    public class CommunicationsController : Controller
    {
        private readonly DefaultContext _context;

        public CommunicationsController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Communications
        public async Task<IActionResult> Index()
        {
            return View(await _context.Communication.ToListAsync());
        }

        // GET: Communications/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communication = await _context.Communication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (communication == null)
            {
                return NotFound();
            }

            return View(communication);
        }

        // GET: Communications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Communications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Communication communication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(communication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(communication);
        }

        // GET: Communications/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communication = await _context.Communication.FindAsync(id);
            if (communication == null)
            {
                return NotFound();
            }
            return View(communication);
        }

        // POST: Communications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Communication communication)
        {
            if (id != communication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(communication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunicationExists(communication.Id))
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
            return View(communication);
        }

        // GET: Communications/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communication = await _context.Communication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (communication == null)
            {
                return NotFound();
            }

            return View(communication);
        }

        // POST: Communications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var communication = await _context.Communication.FindAsync(id);
            _context.Communication.Remove(communication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunicationExists(long? id)
        {
            return _context.Communication.Any(e => e.Id == id);
        }
    }
}
