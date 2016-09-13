using HGV.AD.Web.Data;
using HGV.AD.Web.Models.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HGV.AD.Web.Models.Statistics;

namespace HGV.AD.Web.Services
{
    public class TransferService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public TransferService(
            ApplicationDbContext dbcontext,
            ILoggerFactory loggerFactory
        )
        {
            _dbContext = dbcontext;
            _logger = loggerFactory.CreateLogger<TransferService>();
        }

        public void Transfer()
        {
            HeroTrends();
            AbilityTrends();
            AbilityComboTrends();
            HeroComboTrends();
        }

        private void HeroTrends()
        {
            var collection1 = _dbContext.CurrentHeroTrends.Join(_dbContext.PerviousHeroTrends, _ => _.HeroId, _ => _.HeroId, (lhs, rhs) => new { Item1 = lhs, Item2 = rhs }).ToList();
            foreach (var _ in collection1)
            {
                _.Item2.Kills = _.Item1.Kills;
                _.Item2.Deaths = _.Item1.Deaths;
                _.Item2.Assists = _.Item1.Assists;
                _.Item2.Wins = _.Item1.Wins;
                _.Item2.Loses = _.Item1.Loses;
                _.Item2.Total = _.Item1.Total;

                _dbContext.SaveChanges();
            }

            var collection2 = _dbContext.NextHeroTrends.Join(_dbContext.CurrentHeroTrends, _ => _.HeroId, _ => _.HeroId, (lhs, rhs) => new { Item1 = lhs, Item2 = rhs }).ToList();
            foreach (var _ in collection2)
            {
                _.Item2.Kills = _.Item1.Kills;
                _.Item2.Deaths = _.Item1.Deaths;
                _.Item2.Assists = _.Item1.Assists;
                _.Item2.Wins = _.Item1.Wins;
                _.Item2.Loses = _.Item1.Loses;
                _.Item2.Total = _.Item1.Total;

                _dbContext.SaveChanges();
            }

            var collection3 = _dbContext.NextHeroTrends.ToList();
            foreach (var _ in collection3)
            {
                _.Kills = 0;
                _.Deaths = 0;
                _.Assists = 0;
                _.Wins = 0;
                _.Loses = 0;
                _.Total = 0;

                _dbContext.SaveChanges();
            }
        }

        private void AbilityTrends()
        {
            var collection1 = _dbContext.CurrentAbilityTrends.Join(_dbContext.PerviousAbilityTrends, _ => _.AbilityId, _ => _.AbilityId, (lhs, rhs) => new { Item1 = lhs, Item2 = rhs }).ToList();
            foreach (var _ in collection1)
            {
                _.Item2.Kills = _.Item1.Kills;
                _.Item2.Deaths = _.Item1.Deaths;
                _.Item2.Assists = _.Item1.Assists;
                _.Item2.Wins = _.Item1.Wins;
                _.Item2.Loses = _.Item1.Loses;
                _.Item2.Total = _.Item1.Total;

                _dbContext.SaveChanges();
            }

            var collection2 = _dbContext.NextAbilityTrends.Join(_dbContext.CurrentAbilityTrends, _ => _.AbilityId, _ => _.AbilityId, (lhs, rhs) => new { Item1 = lhs, Item2 = rhs }).ToList();
            foreach (var _ in collection2)
            {
                _.Item2.Kills = _.Item1.Kills;
                _.Item2.Deaths = _.Item1.Deaths;
                _.Item2.Assists = _.Item1.Assists;
                _.Item2.Wins = _.Item1.Wins;
                _.Item2.Loses = _.Item1.Loses;
                _.Item2.Total = _.Item1.Total;

                _dbContext.SaveChanges();
            }

            var collection3 = _dbContext.NextAbilityTrends.ToList();
            foreach (var _ in collection3)
            {
                _.Kills = 0;
                _.Deaths = 0;
                _.Assists = 0;
                _.Wins = 0;
                _.Loses = 0;
                _.Total = 0;

                _dbContext.SaveChanges();
            }
        }

        private void AbilityComboTrends()
        {
            var collection1 = _dbContext.CurrentAbilityComboTrends.Join(_dbContext.PerviousAbilityComboTrends, _ => _.AbilityId ^ _.ComboId, _ => _.AbilityId ^ _.ComboId, (lhs, rhs) => new { Item1 = lhs, Item2 = rhs }).ToList();
            foreach (var _ in collection1)
            {
                _.Item2.Kills = _.Item1.Kills;
                _.Item2.Deaths = _.Item1.Deaths;
                _.Item2.Assists = _.Item1.Assists;
                _.Item2.Wins = _.Item1.Wins;
                _.Item2.Loses = _.Item1.Loses;
                _.Item2.Total = _.Item1.Total;

                _dbContext.SaveChanges();
            }

            var collection2 = _dbContext.NextAbilityComboTrends.Join(_dbContext.CurrentAbilityComboTrends, _ => _.AbilityId ^ _.ComboId, _ => _.AbilityId ^ _.ComboId, (lhs, rhs) => new { Item1 = lhs, Item2 = rhs }).ToList();
            foreach (var _ in collection2)
            {
                _.Item2.Kills = _.Item1.Kills;
                _.Item2.Deaths = _.Item1.Deaths;
                _.Item2.Assists = _.Item1.Assists;
                _.Item2.Wins = _.Item1.Wins;
                _.Item2.Loses = _.Item1.Loses;
                _.Item2.Total = _.Item1.Total;

                _dbContext.SaveChanges();
            }

            var collection3 = _dbContext.NextAbilityComboTrends.ToList();
            foreach (var _ in collection3)
            {
                _.Kills = 0;
                _.Deaths = 0;
                _.Assists = 0;
                _.Wins = 0;
                _.Loses = 0;
                _.Total = 0;

                _dbContext.SaveChanges();
            }
        }

        private void HeroComboTrends()
        {
            var collection1 = _dbContext.CurrentHeroComboTrends.Join(_dbContext.PerviousHeroComboTrends, _ => _.HeroId ^ _.AbilityId, _ => _.HeroId ^ _.AbilityId, (lhs, rhs) => new { Item1 = lhs, Item2 = rhs }).ToList();
            foreach (var _ in collection1)
            {
                _.Item2.Kills = _.Item1.Kills;
                _.Item2.Deaths = _.Item1.Deaths;
                _.Item2.Assists = _.Item1.Assists;
                _.Item2.Wins = _.Item1.Wins;
                _.Item2.Loses = _.Item1.Loses;
                _.Item2.Total = _.Item1.Total;

                _dbContext.SaveChanges();
            }

            var collection2 = _dbContext.NextHeroComboTrends.Join(_dbContext.CurrentHeroComboTrends, _ => _.HeroId ^ _.AbilityId, _ => _.HeroId ^ _.AbilityId, (lhs, rhs) => new { Item1 = lhs, Item2 = rhs }).ToList();
            foreach (var _ in collection2)
            {
                _.Item2.Kills = _.Item1.Kills;
                _.Item2.Deaths = _.Item1.Deaths;
                _.Item2.Assists = _.Item1.Assists;
                _.Item2.Wins = _.Item1.Wins;
                _.Item2.Loses = _.Item1.Loses;
                _.Item2.Total = _.Item1.Total;

                _dbContext.SaveChanges();
            }

            var collection3 = _dbContext.NextHeroComboTrends.ToList();
            foreach (var _ in collection3)
            {
                _.Kills = 0;
                _.Deaths = 0;
                _.Assists = 0;
                _.Wins = 0;
                _.Loses = 0;
                _.Total = 0;

                _dbContext.SaveChanges();
            }
        }
    }
}
