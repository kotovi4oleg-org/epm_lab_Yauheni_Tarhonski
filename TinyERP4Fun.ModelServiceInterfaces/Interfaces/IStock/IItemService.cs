using System.Linq;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;

namespace TinyERP4Fun.Interfaces
{
    public interface IItemsService: IBaseService<Item>
    {
        IQueryable<Ids> GetUnitsIds();
    }
}
