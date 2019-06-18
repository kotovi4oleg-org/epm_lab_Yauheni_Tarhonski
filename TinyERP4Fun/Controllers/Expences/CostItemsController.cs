using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class CostItemsController : Controller
    {
        private readonly ICostItemsService _costItemsService;

        public CostItemsController(ICostItemsService costItemsService)
        {
            _costItemsService = costItemsService;
        }

        // GET: CostItems
        public async Task<IActionResult> Index()
        {
            return View(await _costItemsService.GetListAsync());
        }

        // GET: CostItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _costItemsService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: CostItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CostItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CostItem costItem)
        {
            if (ModelState.IsValid)
            {
                await _costItemsService.AddAsync(costItem);
                return RedirectToAction(nameof(Index));
            }
            return View(costItem);
        }

        // GET: CostItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _costItemsService.GetAsync(id, true);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: CostItems/Edit/5
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
                if(!await _costItemsService.UpdateAsync(costItem)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(costItem);
        }

        // GET: CostItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _costItemsService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: CostItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _costItemsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
