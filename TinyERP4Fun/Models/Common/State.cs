using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Common
{
    public class State : IHaveName, IHaveLongId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
    }
}
