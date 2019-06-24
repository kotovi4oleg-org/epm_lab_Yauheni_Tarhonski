using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Common
{
    public class Currency : IHaveName, IHaveLongId
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Part001Name { get; set; }
        public string Part001Name2 { get; set; }
        public bool Active { get; set; }
        public bool Base { get; set; }
    }
}
