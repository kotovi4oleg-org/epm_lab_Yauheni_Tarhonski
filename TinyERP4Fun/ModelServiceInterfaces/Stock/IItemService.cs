using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IItemService
    {
        Task<Item> GetItem(long? id, bool tracking = false);
        Task AddItem(Item item);
        Task<bool> UpdateItem(Item item);
        Task DeleteItem(long id);
        Task<IEnumerable<Item>> GetItemsList();
        SelectList GetUnitIds();
    }
}
