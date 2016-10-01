using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace HGV.AD.Web.Controllers
{
	public class MainController : Controller
	{
        private readonly ILogger _logger;
        public MainController(
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MainController>();
        }


        public IActionResult Index()
		{
            return RedirectToAction("Index", "Info");
		}

		public IActionResult HandleException()
		{
			return View("Error");
		}

		public IActionResult HandleStatusCode(int id)
		{
			var code = (HttpStatusCode)id;
            switch (code)
			{
				case HttpStatusCode.Forbidden:
					return View("Forbidden");
				case HttpStatusCode.Unauthorized:
					return View("Forbidden");
                case HttpStatusCode.NotFound:
                    return View("NotFound");
                default:
					return View("Error");
			}
		}
	}
}
