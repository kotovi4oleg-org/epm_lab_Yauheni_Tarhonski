using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Stock
{
    public class Item : IHaveName, IHaveLongId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UnitId { get; set; }
        public Unit Unit { get; set; }
        public byte[] Image { get; set; } // ссылка на изображение
        public string ContentType { get; set; } // ссылка на изображение

    }
}
