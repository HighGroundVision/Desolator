using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HGV.AD.Web.Data;

namespace HGV.AD.Web.Controllers
{
    public class AbilitiesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AbilitiesController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var stats = _dbContext.Abilities
                .OrderBy(_ => _.Name)
                .ToList();

            var trends = _dbContext.CurrentAbilityTrends
               .OrderBy(_ => _.Name)
               .ToList();

            var combos = _dbContext.CurrentAbilityTrends
                .Join(_dbContext.PerviousAbilityTrends, _ => _.AbilityId, _ => _.AbilityId, (lhs, rhs) => new { Current = lhs, Pervious = rhs })
                .ToList();

            var change = combos
                .Select(_ => _.Current - _.Pervious)
                .ToList();

            var viewModel = Tuple.Create(stats, trends, change);

            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue == false)
                return RedirectToAction("List");

            var ability = _dbContext.Abilities.FirstOrDefault(_ => _.Id == id);

            var perviousAbilityTrendss = _dbContext.PerviousAbilityTrends.FirstOrDefault(_ => _.AbilityId == id);
            var currentAbilityTrends = _dbContext.CurrentAbilityTrends.FirstOrDefault(_ => _.AbilityId == id);
            var changeAbilityTrends = currentAbilityTrends - perviousAbilityTrendss;

            var combos = _dbContext.CurrentAbilityComboTrends.Where(_ => _.AbilityId == id).Where(_ => _.Total > 0).ToList();

            var query = _dbContext.PerviousAbilityComboTrends.Where(_ => _.AbilityId == id).ToList();
            var change = _dbContext.CurrentAbilityComboTrends.Where(_ => _.AbilityId == id)
                .Join(query, _ => _.ComboId, _ => _.ComboId, (lhs, rhs) => new { Current = lhs, Pervious = rhs })
                .Select(_ => _.Current - _.Pervious)
                .ToList();

            var viewModel = Tuple.Create(ability, currentAbilityTrends, changeAbilityTrends, combos, change);

            return View(viewModel);
        }
    }
}