using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HGV.AD.Web.Data;
using HGV.AD.Web.Models.Statistics;
using Microsoft.EntityFrameworkCore;

namespace HGV.AD.Web.Controllers
{
    public class DraftController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DraftController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var heroes = _dbContext.Heroes.ToList();

            return View(heroes);
        }

        public IActionResult GetDraftPool()
        {
            var collection = _dbContext.Abilities
                .Where(_ => _.Ultimate == true)
                .OrderBy(_ => _.Name)
                .Select(_ => new { Id = _.AbilityId, Identity = _.Identity, HeroId = _.HeroId })
                .ToList();

            return Json(collection);
        }

        public IActionResult GetHeroSummary(int id)
        {
            var entity = _dbContext.Heroes
                .Where(_ => _.HeroId == id)
                .Select(_ => new { Id = _.HeroId, Identity = _.Identity })
                .FirstOrDefault();

            return Json(entity);
        }

        public IActionResult GetHeroesAbilities(int id)
        {
            var collection = _dbContext.Abilities
                .Where(_ => _.HeroId == id)
                .Select(_ => new { Id = _.AbilityId, Identity = _.Identity, HeroId = _.HeroId, Ultimate = _.Ultimate })
                .ToList();

            return Json(collection);
        }

        [HttpPost]
        public IActionResult GetCombosFromPool(IEnumerable<int> abilities)
        {
            var query = _dbContext.CurrentAbilityComboTrends
                .Join(abilities, _ => _.AbilityId, _ => _, (lhs, rhs) => lhs)
                .Where(_ => abilities.Any(__ => __ == _.ComboId))
                .Where(_ => _.Total > 0)
                .ToList();

            var collection = query.GroupBy(_ => new {
                    AbilityId = _.AbilityId > _.ComboId ? _.AbilityId : _.ComboId,
                    ComboId = _.ComboId > _.AbilityId ? _.AbilityId : _.ComboId
                })
                .Select(_ => _.First())
                .ToList();

            return Json(collection);
        }

        [HttpPost]
        public IActionResult GetHeroDetails(int id, IEnumerable<int> abilities)
        {
            var hero = _dbContext.Heroes
                .FirstOrDefault(_ => _.HeroId == id);

            var attributes = _dbContext.HeroAttributeRanks
                .Where(_ => _.HeroId == id)
                .Where(_ => _.Percentage < 0.20 || _.Percentage > 0.80)
                .OrderByDescending(_ => _.Percentage)
                .ToList();

            var trends = _dbContext.CurrentHeroTrends
                .FirstOrDefault(_ => _.HeroId == id);

            var combos = _dbContext.CurrentHeroComboTrends
                .Where(_ => _.HeroId == id)
                .Where(_ => _.Total > 0)
                .Join(abilities, _ => _.AbilityId, _ => _, (lhs, rhs) => lhs)
                .Where(_ => abilities.Any(__ => __ == _.AbilityId))
                .ToList();

            var viewModel = Tuple.Create(hero, attributes, trends, combos);

            return Json(viewModel);
        }

        [HttpPost]
        public IActionResult GetAbilityDetails(int id, IEnumerable<int> abilities)
        {
            var ability = _dbContext.Abilities
                .FirstOrDefault(_ => _.AbilityId == id);

            var trends = _dbContext.CurrentAbilityTrends
                .FirstOrDefault(_ => _.AbilityId == id);

            var combos = _dbContext.CurrentAbilityComboTrends
                .Where(_ => _.AbilityId == id)
                .Where(_ => _.Total > 0)
                .Join(abilities, _ => _.AbilityId, _ => _, (lhs, rhs) => lhs)
                .Where(_ => abilities.Any(__ => __ == _.ComboId))
                .ToList();

            var viewModel = Tuple.Create(ability, trends, combos);

            return Json(viewModel);
        }

    }
}