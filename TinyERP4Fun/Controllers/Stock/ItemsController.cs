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

namespace TinyERP4Fun.Controllers
{
    public class ItemsController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IStockService _stockService;

        public ItemsController(DefaultContext context, IStockService stockService)
        {
            _context = context;
            _stockService = stockService;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Item.Include(i => i.Unit);
            return View(await defaultContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _stockService.GetItemInfo(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitId")] Item item, IList<IFormFile> files)
        {
            if (ModelState.IsValid)
            {

                IFormFile uploadedImage = files.FirstOrDefault();
                if (uploadedImage != null && uploadedImage.ContentType.ToLower().StartsWith("image/"))
                {

                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                    {
                        if (image.Width > Constants.maxImageSize || image.Height > Constants.maxImageSize)
                            return NotFound();
                        item.Image = ms.ToArray();
                        item.ContentType = uploadedImage.ContentType;
                    }
                }

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _stockService.GetItemInfo(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,UnitId")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _stockService.GetItemInfo(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(long id)
        {
            return _context.Item.Any(e => e.Id == id);
        }

        
        [HttpPost]
        public async Task<IActionResult> UploadImage(long? id, IList<IFormFile> files)
        {
            if (id == null)
                return NotFound();


            var item = await _context.Item.FindAsync(id);

            if (item == null)
                return NotFound();


            IFormFile uploadedImage = files.FirstOrDefault();
            if (uploadedImage == null)
            {
                item.Image = null;
                item.ContentType = null;
            }
            else if (uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {
                
                MemoryStream ms = new MemoryStream();
                uploadedImage.OpenReadStream().CopyTo(ms);
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                {
                    if (image.Width > Constants.maxImageSize || image.Height > Constants.maxImageSize)
                        return NotFound();
                    item.Image = ms.ToArray();
                    item.ContentType = uploadedImage.ContentType;
                }
            }

            _context.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = item.Id });
        }
        
        [HttpGet]
        public FileStreamResult ViewImage(long? id)
        {
            if (id == null)
                return null;
            var item = _context.Item.Find(id);

            if (item == null||item.Image == null)
                return null;
            
            MemoryStream ms = new MemoryStream(item.Image);
            return new FileStreamResult(ms, item.ContentType);
        }
    }
}
