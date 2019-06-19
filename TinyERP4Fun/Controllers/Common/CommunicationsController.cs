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
        private readonly ICommunicationsService _communicationsService;
        public CommunicationsController(ICommunicationsService communicationsService)
        {
            _communicationsService = communicationsService;
        }

        // GET: Communications
        public async Task<IActionResult> Index()
        {
            return View(await _communicationsService.GetListAsync());
        }

        // GET: Communications/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _communicationsService.GetAsync(id);
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
                await _communicationsService.AddAsync(communication);
                return RedirectToAction(nameof(Index));
            }
            return View(communication);
        }

        // GET: Communications/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _communicationsService.GetAsync(id,true);
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
                if(!await _communicationsService.UpdateAsync(communication)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(communication);
        }

        // GET: Communications/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _communicationsService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Communications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _communicationsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
