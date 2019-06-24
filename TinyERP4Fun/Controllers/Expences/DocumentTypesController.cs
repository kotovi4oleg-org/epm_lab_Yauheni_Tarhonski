using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesExpences_User)]
    public class DocumentTypesController : Controller
    {
        private readonly IDocumentTypesService _documentTypesService;
        public DocumentTypesController(IDocumentTypesService documentTypesService)
        {
            _documentTypesService = documentTypesService;
        }

        // GET: DocumentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _documentTypesService.GetListAsync());
        }

        // GET: DocumentTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _documentTypesService.GetAsync(id);
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
                await _documentTypesService.AddAsync(documentType);
                return RedirectToAction(nameof(Index));
            }
            return View(documentType);
        }

        // GET: DocumentTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _documentTypesService.GetAsync(id,true);
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
                if (!await _documentTypesService.UpdateAsync(documentType)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(documentType);
        }

        // GET: DocumentTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _documentTypesService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: DocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _documentTypesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
