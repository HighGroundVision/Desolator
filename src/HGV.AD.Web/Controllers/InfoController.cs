using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HGV.AD.Web.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Illusions()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Guide()
        {
            return View();
        }
        
    }
}