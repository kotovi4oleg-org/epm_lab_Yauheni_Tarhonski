using TinyERP4Fun.Interfaces;
using System.Collections.Generic;

namespace TinyERP4Fun.Models.Common
{
    public class Department : IHaveName, IHaveLongId, ICanSetName
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
