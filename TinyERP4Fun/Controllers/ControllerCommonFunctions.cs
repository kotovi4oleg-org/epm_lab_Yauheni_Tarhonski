using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Controllers
{
    public static class ControllerCommonFunctions
    {
        internal static SelectList AddFirstItem(SelectList list)
        {
            List<SelectListItem> _list = list.ToList();
            _list.Insert(0, new SelectListItem() { Value = null, Text = "" });
            return new SelectList(_list, "Value", "Text");
        }
    }
}
