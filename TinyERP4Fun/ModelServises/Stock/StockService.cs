using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ModelServiceInterfaces;
using TinyERP4Fun.ViewModels;

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
        public async Task<List<StockViewModel>> GetGroupedContentAsync(DateTime? fromFilter,
                                                   DateTime? toFilter,
                                                   IEnumerable<long?> itemFilter,
                                                   IEnumerable<long?> warehouseFilter,
                                                   IEnumerable<string> userFilter
                                                   )
        {
            var filteredContext = GetFiltredContent(fromFilter, toFilter, itemFilter, warehouseFilter, userFilter);
            //Это очень красивая строка  объясняет как делать множественный групбай: var result31 = await filteredContext.GroupBy(s => new { s.WarehouseId, s.ItemId }).Select(group => new { group.Key, Count = group.Sum(p=>p.Quantity) }).ToListAsync();
            var result = await filteredContext.GroupBy(s => new { s.WarehouseId, s.ItemId })
                                              .Select(group => new StockViewModel()
                                                                    {
                                                                        Item = group.FirstOrDefault().Item,
                                                                        ItemId = group.Key.ItemId,
                                                                        Warehouse = group.FirstOrDefault().Warehouse,
                                                                        WarehouseId = group.Key.WarehouseId,
                                                                        Quantity = group.Sum(s => s.Quantity)
                                                                    })
                                              .AsNoTracking()
                                              .ToListAsync();
            return result;
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
                if (!ServicesCommonFunctions.EntityExists<Stock>(stock.Id, _context)) return false;
                throw;
            }
        }
        public void ClearHistory(DateTime date)
        {
            throw new NotImplementedException();
        }
        public SelectList GetItemIds()
        {
            return new SelectList(_context.Item.AsNoTracking(), "Id", "Name");
        }
        public SelectList GetUsersIds()
        {
            return new SelectList(_context.Users.AsNoTracking(), "Id", "Email");
        }
        public SelectList GetWarehouseIds()
        {
            return new SelectList(_context.Warehouse.AsNoTracking(), "Id", "Name");
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
            await CheckState(stock, false);
        }
        #endregion Private

    }
}
