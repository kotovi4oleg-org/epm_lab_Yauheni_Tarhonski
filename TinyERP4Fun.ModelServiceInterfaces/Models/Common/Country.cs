using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Common
{
    public class Country : IHaveName, IHaveLongId, ICanSetName
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Country()
        {}
    }
}
