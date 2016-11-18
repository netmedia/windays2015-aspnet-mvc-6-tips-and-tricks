using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Windays2016.EnvironmentAndStartup.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("windayskey", "WINDAYS SESSION KOJI PUCA :)");

            //var valueFromSession = HttpContext.Session.GetString("windayskey");


            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
