using System;
using System.Collections.Generic;
using System.Linq;

using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;

namespace TinyERP4Fun.Interfaces
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
        IQueryable<Ids> GetItemsIds();
        IQueryable<Ids> GetUsersIds();
        IQueryable<Ids> GetWarehousesIds();
    }
}
