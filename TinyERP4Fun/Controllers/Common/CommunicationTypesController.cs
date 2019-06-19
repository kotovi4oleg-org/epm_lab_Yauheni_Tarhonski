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
    public class CommunicationTypesController : Controller
    {
        private readonly ICommunicationTypesService _communicationTypesService;

        public CommunicationTypesController(ICommunicationTypesService communicationTypesService)
        {
            _communicationTypesService = communicationTypesService;
        }
        // GET: CommunicationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _communicationTypesService.GetListAsync());
        }
        // GET: CommunicationTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _communicationTypesService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }
        // GET: CommunicationTypes/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: CommunicationTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CommunicationType communicationType)
        {
            if (ModelState.IsValid)
            {
                await _communicationTypesService.AddAsync(communicationType);
                return RedirectToAction(nameof(Index));
            }
            return View(communicationType);
        }

        // GET: CommunicationTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _communicationTypesService.GetAsync(id, true);
            if (result == null) return NotFound();
            return View(result);
        }
        // POST: CommunicationTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] CommunicationType communicationType)
        {
            if (id != communicationType.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if(!await _communicationTypesService.UpdateAsync(communicationType)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(communicationType);
        }

        // GET: CommunicationTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _communicationTypesService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }
        // POST: CommunicationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _communicationTypesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
