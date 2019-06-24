using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Stock
{
    public class Stock: IHaveLongId
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public Item Item { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Quantity { get; set; }
        public long WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        [DataType(DataType.Date)]
        public DateTime OperDate { get; set; }

    }
}
