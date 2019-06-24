using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Common
{
    public class CommunicationType : IHaveName, IHaveLongId
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
