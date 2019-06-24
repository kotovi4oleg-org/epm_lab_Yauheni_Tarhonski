using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.ModelServises;

namespace TinyERP4Fun.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _itemService.GetListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _itemService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }
        private void SetViewData()
        {
            ViewData["UnitId"] = new SelectList(_itemService.GetUnitsIds(), "Id", "Name");
        }
        // GET: Items/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitId")] Item item, IList<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                ServicesCommonFunctions.AddImage(ref item, files);
                await _itemService.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _itemService.GetAsync(id, true);
            if (result == null) return NotFound();
            SetViewData();
            return View(result);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,UnitId")] Item item)
        {
            if (id != item.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if(!await _itemService.UpdateAsync(item)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _itemService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _itemService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(long? id, IList<IFormFile> files)
        {
            var item = await _itemService.GetAsync(id);
            if (item == null) return NotFound();
            ServicesCommonFunctions.AddImage(ref item, files);
            await _itemService.UpdateAsync(item);
            return RedirectToAction(nameof(Edit), new { id = item.Id });
        }
        
        [HttpGet]
        public async Task<FileStreamResult> ViewImage(long? id)
        {
            var entityTask = await _itemService.GetAsync(id);
            return ServicesCommonFunctions.GetImage(entityTask);
        }
    }
}
