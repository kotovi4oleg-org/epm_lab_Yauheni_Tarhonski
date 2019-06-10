using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.ViewModels
{
    public class PersonViewModel
    {
        public Person Person { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }

    }
}
