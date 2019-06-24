using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Common
{
    public class City : IHaveName, IHaveLongId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long StateId { get; set; }
        public State State { get; set; }
    }
}
