using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;
using TinyERP4Fun.ViewModels;


namespace TinyERP4Fun.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
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
                ItemFilter = _stockService.GetItemIds(),
                WarehouseFilter = _stockService.GetWarehouseIds(),
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
            var result = await _stockService.GetGroupedContentAsync(fromFilter, toFilter, itemFilter, warehouseFilter, emptyStringList);
            StockPaginatedViewModel stockPaginatedViewModel = new StockPaginatedViewModel()
            {
                StockViewModels = PaginatedList<StockViewModel>.Create(result, pageNumber ?? 1, Constants.pageSize),
                ItemFilter = _stockService.GetItemIds(),
                WarehouseFilter = _stockService.GetWarehouseIds(),
                FromFilter = fromFilter,
                ToFilter = toFilter
            };
            return View(stockPaginatedViewModel);
        }
        public async Task<IActionResult> Details(long? id)
        {
            var stock = await _stockService.GetAsync(id);
            if (stock == null) return NotFound();
            return View(stock);
        }
        private void SetViewData()
        {
            ViewData["ItemId"] = _stockService.GetItemIds();
            ViewData["UserId"] = _stockService.GetUsersIds();
            ViewData["WarehouseId"] = _stockService.GetWarehouseIds();
        }
        // GET: Stocks/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Stocks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemId,Quantity,WarehouseId,UserId,OperDate")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                await _stockService.AddAsync(stock);
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var stock = await _stockService.GetAsync(id, true);
            if (stock == null) return NotFound();
            SetViewData();
            return View(stock);
        }

        // POST: Stocks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ItemId,Quantity,WarehouseId,UserId,OperDate")] Stock stock)
        {
            if (id != stock.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!await _stockService.UpdateAsync(stock)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var stock = await _stockService.GetAsync(id);
            if (stock == null) return NotFound();
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _stockService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
