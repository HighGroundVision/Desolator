using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using HGV.AD.Web.Data;

namespace HGV.AD.Web.Controllers
{
    [Authorize]
	public class MatchesController : Controller
	{
        private readonly ApplicationDbContext _dbContext;

        public MatchesController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetHeroes()
        {
            var collection = _dbContext.Heroes.ToList();
            return Json(collection);
        }

        public IActionResult GetAbilities()
        {
            var collection = _dbContext.Abilities.ToList();
            return Json(collection);
        }

        public IActionResult GetTalenets()
        {
            var collection = _dbContext.Talenets.ToList();
            return Json(collection);
        }
    }
}
