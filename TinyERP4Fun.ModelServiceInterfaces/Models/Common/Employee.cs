using TinyERP4Fun.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace TinyERP4Fun.Models.Common
{
    public class Employee : IHaveName, IHaveLongId, ICanSetName
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
