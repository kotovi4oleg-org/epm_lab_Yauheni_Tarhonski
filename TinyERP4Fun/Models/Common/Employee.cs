using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Common
{
    public class Employee : IHaveName, IHaveLongId
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; }
        [Display(Name = "Number")]
        public string Name { get; set; }
        public long? PositionId { get; set; }
        public Position Position { get; set; }
        public string Address { get; set; }
        public long? DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
