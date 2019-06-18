using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly IWarehouseService _warehouseService;
        public WarehousesController( IWarehouseService warehouseService)
        {

            _warehouseService = warehouseService;
        }
        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
            return View(await _warehouseService.GetListAsync());
        }
        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _warehouseService.GetAsync(id);
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
                await _warehouseService.AddAsync(warehouse);
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }
        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _warehouseService.GetAsync(id, true);
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
                if (!await _warehouseService.UpdateAsync(warehouse)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }
        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _warehouseService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }
        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _warehouseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
