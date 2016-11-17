using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HGV.AD.Web.Data;
using HGV.AD.Web.Models.Statistics;

namespace HGV.AD.Web.Controllers
{
    public class HeroesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HeroesController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public ActionResult Index(bool? ts = false)
        {
            if (ts == true)
            {
                var stats = _dbContext.Heroes
                    .OrderBy(_ => _.Name)
                    .ToList();

                var trends = _dbContext.NextHeroTrends
                    .Select(_ => (HeroStatBase)_)
                    .OrderBy(_ => _.Name)
                    .ToList();

                var combos = _dbContext.NextHeroTrends
                    .Join(_dbContext.CurrentHeroTrends, _ => _.HeroId, _ => _.HeroId, (lhs, rhs) => new { Next = lhs, Current = rhs })
                    .ToList();

                var change = combos
                    .Select(_ => _.Next - _.Current)
                    .ToList();

                var viewModel = Tuple.Create(stats, trends, change);

                return View(viewModel);
            }
            else
            {
                var stats = _dbContext.Heroes
                   .OrderBy(_ => _.Name)
                   .ToList();

                var trends = _dbContext.CurrentHeroTrends
                    .Select(_ => (HeroStatBase)_)
                    .OrderBy(_ => _.Name)
                    .ToList();

                var combos = _dbContext.CurrentHeroTrends
                    .Join(_dbContext.PerviousHeroTrends, _ => _.HeroId, _ => _.HeroId, (lhs, rhs) => new { Current = lhs, Pervious = rhs })
                    .ToList();

                var change = combos
                    .Select(_ => _.Current - _.Pervious)
                    .ToList();

                var viewModel = Tuple.Create(stats, trends, change);

                return View(viewModel);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue == false)
                return RedirectToAction("List");

            var hero = _dbContext.Heroes.FirstOrDefault(_ => _.HeroId == id);

            var attributes = _dbContext.HeroAttributeRanks.Where(_ => _.HeroId == id).OrderBy(_ => _.Index).ToList();

            var currentHeroTrends = _dbContext.CurrentHeroTrends.FirstOrDefault(_ => _.HeroId == id);
            var perviousHeroTrends = _dbContext.PerviousHeroTrends.FirstOrDefault(_ => _.HeroId == id);
            var changeHeroTrends = currentHeroTrends -perviousHeroTrends;

            var combos = _dbContext.CurrentHeroComboTrends.Where(_ => _.HeroId == id).Where(_ => _.Total > 0).ToList();

            var query = _dbContext.PerviousHeroComboTrends.Where(_ => _.HeroId == id).ToList();
            var change = _dbContext.CurrentHeroComboTrends.Where(_ => _.HeroId == id)
                .Join(query, _ => _.AbilityId, _ => _.AbilityId, (lhs, rhs) => new { Current = lhs, Pervious = rhs })
                .Select(_ => _.Current - _.Pervious)
                .ToList();

            var viewModel = Tuple.Create(hero, attributes, currentHeroTrends, changeHeroTrends, combos, change);

            return View(viewModel);
        }
    }
}