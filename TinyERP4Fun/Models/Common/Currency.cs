﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Common
{
    public class Currency : IHaveName
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Part001Name { get; set; }
        public string Part001Name2 { get; set; }
        public bool Active { get; set; }
        public bool Base { get; set; }
    }
}