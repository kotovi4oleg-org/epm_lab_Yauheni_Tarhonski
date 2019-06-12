using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Stock
{
    public class Warehouse : IHaveName, IHaveLongId
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
