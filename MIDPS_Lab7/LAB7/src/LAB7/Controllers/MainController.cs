using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LAB7.Controllers
{
    public class MainController : Controller
    {
        // GET: /<controller>/
        public IActionResult Elf()
        {
            using (var context = new Models.MiddleEarthContext())
            {
                List<Models.Elf> items = context.Elf.ToList();
                return View("Elf", items);
            }
        }

        public IActionResult Hobbit()
        {
            using (var context = new Models.MiddleEarthContext())
            {
                List<Models.Hobbit> items = context.Hobbit.ToList();
                return View("Hobbit", items);
            }
        }

        public IActionResult Wizard()
        {
            using (var context = new Models.MiddleEarthContext())
            {
                List<Models.Wizard> items = context.Wizard.ToList();
                return View("Wizard", items);
            }
        }

        public IActionResult Ring()
        {
            using (var context = new Models.MiddleEarthContext())
            {
                List<Models.Ring> items = context.Ring.ToList();
                return View("Ring", items);
            }
        }

        public IActionResult Orc()
        {
            using (var context = new Models.MiddleEarthContext())
            {
                List<Models.Orc> items = context.Orc.ToList();
                return View("Orc", items);
            }
        }
    }
}
