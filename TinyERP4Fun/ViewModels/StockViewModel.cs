using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;

namespace TinyERP4Fun.ViewModels
{
    public class StockViewModel
    {
        public long ItemId { get; set; }
        public Item Item { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Quantity { get; set; }
        public long WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

    }
    public class StockPaginatedViewModel
    {
        public PaginatedList<StockViewModel> StockViewModels { get; set; }
        public SelectList ItemFilter { get; set; }
        public SelectList WarehouseFilter { get; set; }
        public DateTime? FromFilter { get; set; }
        public DateTime? ToFilter { get; set; }
    }
    public class StockMovementsViewModel
    {
        public PaginatedList<Stock> Stock { get; set; }
        public SelectList ItemFilter { get; set; }
        public SelectList WarehouseFilter { get; set; }
        public DateTime? FromFilter { get; set; }
        public DateTime? ToFilter { get; set; }
    }
}
