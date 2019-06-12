using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Common
{
    public class Department : IHaveName, IHaveLongId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
