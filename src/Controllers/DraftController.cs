using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HGV.Desolator.Models;

namespace HGV.Desolator.Controllers
{
    public class DraftController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Pool");
        }

        public IActionResult Pool()
        {
            return View();
        }
    }
}
