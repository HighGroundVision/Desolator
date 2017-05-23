using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HGV.AD.Web.Data;
using HGV.AD.Web.Models.Trends;
using Microsoft.EntityFrameworkCore;
using HGV.AD.Web.Models.Attributes;

namespace HGV.AD.Web.Controllers
{
    public class DraftController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly List<int> _filterCauseSteamSucks = new List<int>() { 114, 67, 105, 98, 100 };

        public DraftController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDraftPool()
        {
            var heroes = _dbContext.Heroes;

            var collection = _dbContext.Abilities
                .Where(_ => _.Ultimate == true)
                .OrderBy(_ => _.Name)
                .Join(heroes, _ => _.HeroId, _ => _.HeroId, (lhs, rhs) => new { Ability = lhs, Hero = rhs })
                .Select(_ => new { AbilityId = _.Ability.AbilityId, AbilityIdentity = _.Ability.Identity, HeroId = _.Ability.HeroId, HeroIdentity = _.Hero.Identity, })
                .ToList();

            return Json(collection);
        }

        public IActionResult Recommendations(int heroId, string draft)
        {
            return View();
        }

        public IActionResult GetHeroDetails(int heroId)
        {
            var hero = _dbContext.Heroes.FirstOrDefault(_ => _.HeroId == heroId);

            var talenets = _dbContext.Talenets
                .Where(_ => _.HeroId == heroId)
                .ToList();

            var attributesBad = _dbContext.HeroAttributeRanks
                .Where(_ => _.HeroId == heroId)
                .Where(_ => !_.Name.Contains("Base"))
                .Where(_ => _.Percentage < 0.50)
                .OrderByDescending(_ => _.Percentage)
                .Take(4)
                .ToList();

            var attributesGood = _dbContext.HeroAttributeRanks
                .Where(_ => _.HeroId == heroId)
                .Where(_ => !_.Name.Contains("Base"))
                .Where(_ => _.Percentage > 0.50)
                .OrderByDescending(_ => _.Percentage)
                .Take(4)
                .ToList();

            var viewModel = Tuple.Create(hero, talenets, attributesGood, attributesBad);

            return Json(viewModel);
        }

        public IActionResult GetAbilityPoolFromDraft(string draft)
        {
            var collection = new List<AbilityAttributes>();

            var ultimates = draft.Split(',')
                .Select(_ => int.Parse(_))
                .ToList();

            foreach (var Id in ultimates)
            {
                var entity = _dbContext.Abilities.Find(Id);
                var abilities = _dbContext.Abilities.Where(_ => _.HeroId == entity.HeroId && _.AbilityId != entity.AbilityId).ToList();
                collection.AddRange(abilities);
                collection.Add(entity);
            }

            return Json(collection);
        }

        public IActionResult GetAbilityTrendsFromDraft(int heroId, string draft)
        {
            var ultimates = draft.Split(',')
                .Select(_ => int.Parse(_))
                .ToList();

            var abilities = _dbContext.Abilities
                .Where(_ => ultimates.Contains(_.AbilityId))
                .Where(_ => _.HeroId != heroId)
                .Join(_dbContext.Abilities, _ => _.HeroId, _ => _.HeroId, (lhs, rhs) => rhs.AbilityId)
                .ToList();

            var trends = _dbContext.CurrentHeroComboTrends
                .Where(_ => _.HeroId == heroId)
                .Where(_ => abilities.Contains(_.AbilityId))
                .ToList();

            return Json(trends);
        }

        public IActionResult GetComboTrendsFromDraft(string draft)
        {
            var ultimates = draft.Split(',')
                .Select(_ => int.Parse(_))
                .ToList();

            var abilities = _dbContext.Abilities
                .Where(_ => ultimates.Contains(_.AbilityId))
                .Join(_dbContext.Abilities, _ => _.HeroId, _ => _.HeroId, (lhs, rhs) => rhs.AbilityId)
                .ToList();

            var combos = _dbContext.CurrentAbilityComboTrends
                .Where(_ => _.SameSource == false)
                .Where(_ => abilities.Contains(_.AbilityId))
                .Where(_ => abilities.Contains(_.ComboId))
                .ToList();

            return Json(combos);
        }

        public IActionResult Live(int matchid)
        {
            return View();
        }
    }
}