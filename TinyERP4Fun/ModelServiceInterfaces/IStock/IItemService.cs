using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Stock;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IItemService: IAllServises<Item>, ISimpleList<Item>
    {
        SelectList GetUnitIds();
    }
}
