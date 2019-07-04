using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Expenses
{
    public class BusinessDirection : IHaveName, IHaveLongId, ICanSetName
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
