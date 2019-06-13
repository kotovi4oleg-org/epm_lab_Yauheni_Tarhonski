using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class CommunicationsController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IGeneralService _generalService;

        public CommunicationsController(DefaultContext context, IGeneralService generalService)
        {
            _context = context;
            _generalService = generalService;
        }

        // GET: Communications
        public async Task<IActionResult> Index()
        {
            return View(await _context.Communication.ToListAsync());
        }

        // GET: Communications/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _generalService.GetObject<Communication>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: Communications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Communications/Create
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
            var result = await _generalService.GetObject<Communication>(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Communications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Communication communication)
        {
            if (id != communication.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(communication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunicationExists(communication.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(communication);
        }

        // GET: Communications/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _generalService.GetObject<Communication>(id);
            if (result == null) return NotFound();
            return View(result);
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
