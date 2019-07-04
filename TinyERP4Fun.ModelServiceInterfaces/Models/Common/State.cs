using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Common
{
    public class State : IHaveName, IHaveLongId, ICanSetName
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
    }
}
