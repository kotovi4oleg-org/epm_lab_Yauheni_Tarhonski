using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesExpences_User)]
    public class DocumentTypesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IGeneralService _generalService;

        public DocumentTypesController(DefaultContext context, IGeneralService generalService)
        {
            _context = context;
            _generalService = generalService;
        }

        // GET: DocumentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentType.ToListAsync());
        }

        // GET: DocumentTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _generalService.GetObject<DocumentType>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: DocumentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentType);
        }

        // GET: DocumentTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _generalService.GetObject<DocumentType>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: DocumentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] DocumentType documentType)
        {
            if (id != documentType.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentTypeExists(documentType.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(documentType);
        }

        // GET: DocumentTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _generalService.GetObject<DocumentType>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: DocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var documentType = await _context.DocumentType.FindAsync(id);
            _context.DocumentType.Remove(documentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentTypeExists(long? id)
        {
            return _context.DocumentType.Any(e => e.Id == id);
        }
    }
}
