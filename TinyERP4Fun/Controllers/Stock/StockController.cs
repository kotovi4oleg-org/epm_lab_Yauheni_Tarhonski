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
        
        public IActionResult Index(int? pageNumber,
                                   IEnumerable<long?> itemFilter,
                                   IEnumerable<long?> warehouseFilter,
                                   DateTime? fromFilter,
                                   DateTime? toFilter
                                   )
        {

            #region IQueryable not works as awaited, SelectMany returns null
            /*
            var defaultContext = _context.Stock.Include(s => s.Item)
                                               .Include(s => s.User)
                                               .Include(s => s.Warehouse)
                                               .GroupBy(s => s.Warehouse)
                                               .Select(x=> x.GroupBy(y => y.Item).Select(z => new StockAddViewModel()
                                                                                               {
                                                                                                   Item = z.Key,
                                                                                                   ItemId = z.Key.Id,
                                                                                                   Warehouse = x.Key,
                                                                                                   WarehouseId = x.Key.Id,
                                                                                                   Quantity = z.Sum(p => p.Quantity)
                                                                                               }));
            /**/
            #endregion
            var emptyStringList = new string[] { };
            var filteredContext = _stockService.GetFiltredContent(fromFilter, toFilter, itemFilter, warehouseFilter, emptyStringList);

            IEnumerable<IEnumerable<StockViewModel>> defaultContext = filteredContext
                                               .GroupBy(s => s.Warehouse)
                                               .Select(x => x.GroupBy(y => y.Item).Select(z => new StockViewModel()
                                                                                               {
                                                                                                   Item = z.Key,
                                                                                                   ItemId = z.Key.Id,
                                                                                                   Warehouse = x.Key,
                                                                                                   WarehouseId = x.Key.Id,
                                                                                                   Quantity = z.Sum(p => p.Quantity)
                                                                                               }));
            var result = defaultContext.SelectMany(x=>x);
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
        public IActionResult IndexOld(int? pageNumber)
        {
            var defaultContext = _context.Stock.Include(s => s.Item).Include(s => s.User).Include(s => s.Warehouse).GroupBy(s => s.Warehouse);
            List<StockViewModel> model = new List<StockViewModel>();
            foreach (var wh in defaultContext)
            {
                var warehouse = wh.Select(x => x.Warehouse).FirstOrDefault();
                foreach (var it in wh.GroupBy(w => w.Item))
                {
                    var item = it.Select(x => x.Item).FirstOrDefault();
                    StockViewModel stockAddViewModel = new StockViewModel()
                    {
                        Item = item,
                        ItemId = item.Id,
                        Warehouse = warehouse,
                        WarehouseId = warehouse.Id,
                        Quantity = it.Sum(x => x.Quantity)
                    };
                    model.Add(stockAddViewModel);
                }
            }
            return View(model);
        }
        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var stock = await _stockService.Get(id);
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
            var stock = await _stockService.Get(id);
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
            var stock = await _stockService.Get(id);
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
