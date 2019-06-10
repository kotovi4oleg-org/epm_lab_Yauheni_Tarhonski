using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TinyERP4Fun.Controllers.Common
{
    public class LanguageController : Controller
    {
        public IActionResult EN()
        {
            Localization.currentLocalizatin = Localization.englishLocalizatin;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RU()
        {
            Localization.currentLocalizatin = Localization.russianLocalizatin;
            return RedirectToAction("Index", "Home");
        }
    }
}