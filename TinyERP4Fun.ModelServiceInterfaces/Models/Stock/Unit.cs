using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Stock
{
    public class Unit : IHaveName, IHaveLongId, ICanSetName
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
