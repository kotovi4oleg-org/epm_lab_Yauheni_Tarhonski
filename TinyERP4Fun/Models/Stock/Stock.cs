using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;

namespace TinyERP4Fun.Models.Stock
{
    public class Stock
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
