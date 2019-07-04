using System.ComponentModel.DataAnnotations;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Common
{
    public class Communication : IHaveName, IHaveLongId, ICanSetName
    {
        public long Id { get; set; }
        public CommunicationType CommunicationType { get; set; }
        [Display(Name = "Number")]
        public string Name { get; set; }
        public long? PersonId { get; set; }
        public Person Person { get; set; }
        public long? CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
