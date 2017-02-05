using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LAB7.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("Index", "");
        }

        public IActionResult Login(string userName, string password)
        {
            using (var context = new Models.UsersContext())
            {
                var user = context.Users.FirstOrDefault(b => b.UserName == userName && b.Password == password);
                if (user != null)
                {
                    return RedirectToAction("Elf", "Main");
                }
                return View("Index","Your password or username is wrong. Check them again.");
            }
        }
    }

}
