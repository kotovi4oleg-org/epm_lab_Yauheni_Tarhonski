using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.ModelServises
{
    public class StockService : BaseService<Stock>, IStockService
    {
        public StockService(DefaultContext context) : base(context)
        {
        }

        #region Public
        public override async Task<Stock> GetAsync(long? id, bool tracking = false)
        {
            if (id == null) return null;
            if (tracking)
                return await _context.Stock
                                     .Include(s => s.Item)
                                     .Include(s => s.User)
                                     .Include(s => s.Warehouse)
                                     .SingleOrDefaultAsync(s => s.Id == id);
            else
                return await _context.Stock
                                     .Include(s => s.Item)
                                     .Include(s => s.User)
                                     .Include(s => s.Warehouse)
                                     .AsNoTracking()
                                     .SingleOrDefaultAsync(s => s.Id == id);
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
        public override async Task AddAsync(Stock stock)
        {
            if (stock.Quantity < 0) await CheckAddState(stock);
            _context.Add(stock);
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(long id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock.Quantity > 0) await CheckDelState(stock);
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
        }
        public override async Task<bool> UpdateAsync(Stock stock)
        {
            try
            {
                var oldstock = await _context.Stock.AsNoTracking().FirstOrDefaultAsync(s => s.Id == stock.Id);

                if (oldstock.WarehouseId != stock.WarehouseId || oldstock.ItemId != stock.ItemId)
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
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(stock.Id, _context)) return false;
                throw;
            }
        }
        public void ClearHistory(DateTime date)
        {
            throw new NotImplementedException();
        }
        public IQueryable<Ids> GetItemsIds()
        {
            return _context.Item.AsNoTracking().Select(x=>new Ids(x.Id.ToString(),x.Name));
        }
        public IQueryable<Ids> GetUsersIds()
        {
            return _context.Users.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Email));
        }
        public IQueryable<Ids> GetWarehousesIds()
        {
            return _context.Warehouse.AsNoTracking().Select(x => new Ids(x.Id.ToString(), x.Name));
        }
        #endregion Public

        #region Private
        private async Task CheckState(Stock stock, bool del)
        {
            decimal quantity = del ? 0 : stock.Quantity;
            var sumBefore = quantity + await _context.Stock.Where(x => x.ItemId == stock.ItemId &&
                                                x.WarehouseId == stock.WarehouseId &&
                                                x.OperDate <= stock.OperDate &&
                                                x.Id != stock.Id
                                                ).SumAsync(x => x.Quantity);
            if (sumBefore < 0) throw new ArgumentException("st01:Wrong balance on date " + stock.OperDate + "Quantity = " + sumBefore.ToString());
 
            var sumCheck = _context.Stock
                       .Where(x => x.ItemId == stock.ItemId && x.WarehouseId == stock.WarehouseId && x.OperDate > stock.OperDate && x.Id != stock.Id);

            var sumCheck2 = from first in sumCheck
                                from second in sumCheck
                                where first.OperDate >= second.OperDate
                                select new { first.OperDate, Sum = second.Quantity, second.Id }
                                ;
            var sumCheck3 = sumCheck2.GroupBy(x => new { x.OperDate, x.Id }).OrderBy(x => x.Key.OperDate);
            var sumCheck4 = sumCheck3.Select(x => new { x.Key.OperDate, x.FirstOrDefault().Sum });
            var sumCheck5 = sumCheck4.GroupBy(x => x.OperDate).Select(x => new { x.Key, Sum = x.Sum(y => y.Sum) });
            var sumCheck6 = sumCheck5.Where(x =>  x.Sum < - sumBefore);
            
            if (sumCheck6.Count() > 0)
                throw new ArgumentException("st02:Wrong balance on date " + sumCheck6.First().Key.ToString()  
                                            + "Quantity = " + (sumCheck6.First().Sum + sumBefore).ToString());

        }
        private class DateDecimal
        {
            public DateTime Date { get; set; }
            public decimal Sum { get; set; }
            public DateDecimal (DateTime date, decimal sum)
            {
                Date = date; Sum = sum;
            }
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
            await CheckState(stock, false);
        }
        #endregion Private

    }
}
