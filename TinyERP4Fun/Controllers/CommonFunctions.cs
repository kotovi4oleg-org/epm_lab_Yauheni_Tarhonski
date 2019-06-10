using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using TinyERP4Fun.Data;

namespace TinyERP4Fun.Controllers
{
    
    public static class CommonFunctions
    {
        public static class DefaultContextOptions
        {
            public static DbContextOptions<DefaultContext> GetOptions()
            {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                var config = builder.Build();
                string connectionString = config.GetConnectionString("DefaultConnection");
                var optionsBuilder = new DbContextOptionsBuilder<DefaultContext>();
                var options = optionsBuilder.UseSqlServer(connectionString).Options;
                return options;
            }
        }
        internal static SelectList AddFirstItem(SelectList list)
                {
                    List<SelectListItem> _list = list.ToList();
                    _list.Insert(0, new SelectListItem() { Value = null, Text = "" });
                    return new SelectList(_list, "Value", "Text");
                }
    }
}
