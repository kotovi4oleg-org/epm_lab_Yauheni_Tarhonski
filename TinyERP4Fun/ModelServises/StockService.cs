using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class StockService: IStockService
    {
        private readonly DefaultContext _context;

        public StockService(DefaultContext context)
        {
            _context = context;
        }
        public async Task<Stock> Get(long? id)
        {
            if (id == null) return null;
            return await _context.Stock
                                 .Include(s => s.Item)
                                 .Include(s => s.User)
                                 .Include(s => s.Warehouse)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }
        public IQueryable<Stock> GetFiltredContent(DateTime? fromFilter,
                                                   DateTime? toFilter,
                                                   IEnumerable<long?> itemFilter,
                                                   IEnumerable<long?> warehouseFilter,
                                                   IEnumerable<string> userFilter
                                                   )
        {
            IQueryable<Stock> filteredContext = _context.Stock.Include(s => s.Item).Include(s => s.Warehouse).Include(s => s.User);
            if (itemFilter.Count() != 0) filteredContext = filteredContext.Where(x => itemFilter.Contains(x.ItemId));
            if (warehouseFilter.Count() != 0) filteredContext = filteredContext.Where(x => warehouseFilter.Contains(x.WarehouseId));
            if (userFilter.Count() != 0) filteredContext = filteredContext.Where(x => userFilter.Contains(x.UserId));
            if (fromFilter != null) filteredContext = filteredContext.Where(x => x.OperDate >= fromFilter);
            if (toFilter != null) filteredContext = filteredContext.Where(x => x.OperDate <= toFilter);

            return filteredContext;
        }
        public async Task Add(Stock stock)
        {
            if (stock.Quantity < 0) await CheckAddState(stock);
            _context.Add(stock);
            await _context.SaveChangesAsync();
        }
        public async Task Remove(long id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock.Quantity > 0) await CheckDelState(stock);
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Stock stock)
        {

            var oldstock = await _context.Stock.AsNoTracking().FirstOrDefaultAsync(s => s.Id == stock.Id);

            if (oldstock.WarehouseId!=stock.WarehouseId|| oldstock.ItemId != stock.ItemId)
            {
                await CheckDelState(oldstock);
                await CheckAddState(stock);
            }
            else
            {
                await CheckUpdateState(stock);
            }
            
            await CheckUpdateState(stock);
            _context.Update(stock);
            await _context.SaveChangesAsync();
        }

        public void ClearHistory(DateTime date)
        {
        }

        private async Task CheckState(Stock stock,bool del)
        {
            decimal quantity = del ? 0 : stock.Quantity;
            var sumBefore = quantity + await _context.Stock.Where(x => x.ItemId == stock.ItemId &&
                                                x.WarehouseId == stock.WarehouseId &&
                                                x.OperDate <= stock.OperDate &&
                                                x.Id != stock.Id
                                                ).SumAsync(x => x.Quantity);
            if (sumBefore < 0) throw new ArgumentException("st01:Wrong balance on date " + stock.OperDate + "Quantity = " + sumBefore.ToString());

            var sumCheckAll = _context.Stock
                       .Where(x => x.ItemId == stock.ItemId && x.WarehouseId == stock.WarehouseId && x.OperDate > stock.OperDate && x.Id != stock.Id)
                       .GroupBy(x => x.OperDate)
                       .OrderBy(x => x.Key)
                       .Select(x => new { date = x.Key, sum = x.Sum(y => y.Quantity) })
                       .Select(x => new { x.date, sum = sumBefore + x.sum });
            var sumCheck = sumCheckAll.Where(x => x.sum < 0);
            if (sumCheck.Count() > 0) throw new ArgumentException("st02:Wrong balance on date " + sumCheck.First().date.ToString() + "Quantity = " + sumCheck.First().sum.ToString());
        }
        private async Task CheckAddState(Stock stock)
        {
            await CheckState(stock, false);
        }
        private async Task CheckDelState(Stock stock)
        {
            await CheckState(stock, true);
        }
        private async Task CheckUpdateState(Stock stock)
        {
            await CheckState(stock,false);
        }

    }
}
