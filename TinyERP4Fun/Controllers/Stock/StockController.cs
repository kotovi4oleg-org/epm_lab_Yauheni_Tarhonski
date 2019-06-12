using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;
using TinyERP4Fun.ViewModels;


namespace TinyERP4Fun.Controllers
{
    public class StockController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IStockService _stockService;

        public StockController(DefaultContext context, IStockService stockService)
        {
            _context = context;
            _stockService = stockService;
        }


        // GET: Stocks
        public async Task<IActionResult> Movements(int? pageNumber,
                                                   IEnumerable<long?> itemFilter,
                                                   IEnumerable<long?> warehouseFilter,
                                                   DateTime? fromFilter,
                                                   DateTime? toFilter
                                                   )
        {
            var emptyStringList = new string[] { };
            var filteredContext = _stockService.GetFiltredContent(fromFilter, toFilter, itemFilter, warehouseFilter, emptyStringList);

            StockMovementsViewModel stockMovementsViewModel = new StockMovementsViewModel()
            {
                Stock = await PaginatedList<Stock>.CreateAsync(filteredContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize),
                ItemFilter = new SelectList(_context.Item, "Id", "Name"),
                WarehouseFilter = new SelectList(_context.Warehouse, "Id", "Name"),
                FromFilter = fromFilter,
                ToFilter = toFilter
            };
            return View(stockMovementsViewModel);
        }
        
        public async Task<IActionResult> Index(int? pageNumber,
                                   IEnumerable<long?> itemFilter,
                                   IEnumerable<long?> warehouseFilter,
                                   DateTime? fromFilter,
                                   DateTime? toFilter
                                   )
        {
            var emptyStringList = new string[] { };
            var filteredContext = _stockService.GetFiltredContent(fromFilter, toFilter, itemFilter, warehouseFilter, emptyStringList);
            //Это очень красивая строка  объясняет как делать множественный групбай: var result31 = await filteredContext.GroupBy(s => new { s.WarehouseId, s.ItemId }).Select(group => new { group.Key, Count = group.Sum(p=>p.Quantity) }).ToListAsync();
            var result = await filteredContext.GroupBy(s => new { s.WarehouseId, s.ItemId })
                                                .Select(group => new StockViewModel()
                                                {
                                                    Item = group.FirstOrDefault().Item,
                                                    ItemId = group.Key.ItemId,
                                                    Warehouse = group.FirstOrDefault().Warehouse,
                                                    WarehouseId = group.Key.WarehouseId,
                                                    Quantity = group.Sum(s => s.Quantity)
                                                }).ToListAsync();
            StockPaginatedViewModel stockPaginatedViewModel = new StockPaginatedViewModel()
            {
                StockViewModels = PaginatedList<StockViewModel>.Create(result, pageNumber ?? 1, Constants.pageSize),
                ItemFilter = new SelectList(_context.Item, "Id", "Name"),
                WarehouseFilter = new SelectList(_context.Warehouse, "Id", "Name"),
                FromFilter = fromFilter,
                ToFilter = toFilter
            };

            ViewData["ItemId"] = new SelectList(_context.Item, "Id", "Name");
            return View(stockPaginatedViewModel);
        }
        public async Task<IActionResult> Details(long? id)
        {
            var stock = await _stockService.GetStockInfo(id);
            if (stock == null) return NotFound();
            return View(stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Item, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "Id", "Name");
            return View();
        }

        // POST: Stocks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemId,Quantity,WarehouseId,UserId,OperDate")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                await _stockService.Add(stock);
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var stock = await _stockService.GetStockInfo(id);
            if (stock == null)
                return NotFound();

            ViewData["ItemId"] = new SelectList(_context.Item, "Id", "Name", stock.ItemId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", stock.UserId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouse, "Id", "Name", stock.WarehouseId);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ItemId,Quantity,WarehouseId,UserId,OperDate")] Stock stock)
        {
            if (id != stock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _stockService.Update(stock);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.Id))
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
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var stock = await _stockService.GetStockInfo(id);
            if (stock == null) return NotFound();
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _stockService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(long id)
        {
            return _context.Stock.Any(e => e.Id == id);
        }
    }
}
