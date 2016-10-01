using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HGV.AD.Web.Controllers
{
    public class DonateController : Controller
    {
        public IActionResult Thanks()
        {
            return View();
        }
    }
}