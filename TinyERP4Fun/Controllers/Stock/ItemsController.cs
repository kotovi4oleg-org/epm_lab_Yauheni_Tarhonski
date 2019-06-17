using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;
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
            return View(await _itemService.GetItemsList());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _itemService.GetItem(id);
            if (result == null) return NotFound();
            return View(result);
        }
        private void SetViewData()
        {
            ViewData["UnitId"] = _itemService.GetUnitIds();
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
                await _itemService.AddItem(item);
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _itemService.GetItem(id, true);
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
                if(!await _itemService.UpdateItem(item)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _itemService.GetItem(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _itemService.DeleteItem(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(long? id, IList<IFormFile> files)
        {
            var item = await _itemService.GetItem(id);
            if (item == null) return NotFound();
            ServicesCommonFunctions.AddImage(ref item, files);
            await _itemService.UpdateItem(item);
            return RedirectToAction(nameof(Edit), new { id = item.Id });
        }
        
        [HttpGet]
        public FileStreamResult ViewImage(long? id)
        {
            var entityTask = _itemService.GetItem(id); 
            Task.WaitAll(entityTask);
            return ServicesCommonFunctions.GetImage(entityTask.Result);
        }
    }
}
