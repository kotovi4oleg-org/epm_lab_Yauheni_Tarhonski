using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Common
{
    public class City : IHaveName
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long StateId { get; set; }
        public State State { get; set; }
    }
}
