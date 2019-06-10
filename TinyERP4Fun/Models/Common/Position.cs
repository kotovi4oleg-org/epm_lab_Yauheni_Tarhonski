using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Common
{
    public class Position : IHaveName
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
