using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TinyERP4Fun.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult EN()
        {
            Localization.SetEN();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RU()
        {
            Localization.SetRU();
            return RedirectToAction("Index", "Home");
        }
    }
}