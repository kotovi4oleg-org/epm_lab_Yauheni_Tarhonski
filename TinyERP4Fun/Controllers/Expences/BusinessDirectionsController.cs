using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class BusinessDirectionsController : Controller
    {
        private readonly IBusinessDirectionsService _businessDirectionsService;

        public BusinessDirectionsController(IBusinessDirectionsService businessDirectionsService)
        {
            _businessDirectionsService = businessDirectionsService;
        }

        // GET: BusinessDirections
        public async Task<IActionResult> Index()
        {
            return View(await _businessDirectionsService.GetListAsync());
        }

        // GET: BusinessDirections/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _businessDirectionsService.GetAsync(id);
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
                await _businessDirectionsService.AddAsync(businessDirection);
                return RedirectToAction(nameof(Index));
            }
            return View(businessDirection);
        }

        // GET: BusinessDirections/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _businessDirectionsService.GetAsync(id, true);
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
                if (!await _businessDirectionsService.UpdateAsync(businessDirection)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(businessDirection);
        }

        // GET: BusinessDirections/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _businessDirectionsService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: BusinessDirections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _businessDirectionsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
