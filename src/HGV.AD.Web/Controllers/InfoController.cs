using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HGV.AD.Web.Data;
using HGV.AD.Web.Models.Statistics;

namespace HGV.AD.Web.Controllers
{
    public class InfoController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public InfoController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        
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

        public IActionResult History()
        {
            var query = _dbContext.Batchs
                .GroupBy(_ => _.DateProcessed.Date)
                .OrderByDescending(_ => _.Key)
                .Take(14)
                .ToList();

            var history = query
                .Select(_ => new Batch { DateProcessed = _.Key, NumberOfAD = _.Sum(__ => __.NumberOfAD), NumberOfMatches = _.Sum(__ => __.NumberOfMatches) })
                .ToList();

            return View(history);
        }


    }
}