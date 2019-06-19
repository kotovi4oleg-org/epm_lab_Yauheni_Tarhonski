using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.ViewModels;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IStockService : IBaseService<Stock>
    {
        void ClearHistory(DateTime date);
        IQueryable<Stock> GetFiltredContent(DateTime? fromFilter,
                                            DateTime? toFilter,
                                            IEnumerable<long?> itemFilter,
                                            IEnumerable<long?> warehouseFilter,
                                            IEnumerable<string> userFilter
                                            );
        Task<List<StockViewModel>> GetGroupedContentAsync(DateTime? fromFilter,
                                                     DateTime? toFilter,
                                                     IEnumerable<long?> itemFilter,
                                                     IEnumerable<long?> warehouseFilter,
                                                     IEnumerable<string> userFilter
                                                     );
        SelectList GetItemIds();
        SelectList GetUsersIds();
        SelectList GetWarehouseIds();
    }
}
