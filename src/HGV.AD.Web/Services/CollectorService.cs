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
using HGV.AD.Web.Models.DotaApi;
using HGV.AD.Web.Models.Statistics;
using System.Threading;
using Hangfire;
using HGV.AD.Web.Models.Checkpoints;
using Microsoft.Extensions.Options;
using HGV.AD.Web.Configuration;

namespace HGV.AD.Web.Services
{
    public class CollectorService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IOptions<Site> _siteSettings;

        //private readonly string DotaApiKey;

        public CollectorService(
            ApplicationDbContext dbcontext,
            IOptions<Site> siteSettings,
            ILoggerFactory loggerFactory
        )
        {
            _dbContext = dbcontext;
            _logger = loggerFactory.CreateLogger<CollectorService>();

            _siteSettings = siteSettings;
        }

        [AutomaticRetry(Attempts = 0)]
        public void Collect()
        { 
            var next = GetNextInSquence();

            var checkpoint = _dbContext.Checkpoints.FirstOrDefault(_ => _.MatchId == next.Item1);
            if (checkpoint != null)
            {
                _logger.LogDebug("Already Processed Match: {0}", next.Item1);
                return;
            }

            ProcessMatches(next.Item2);
        }

        private Tuple<long,long> GetNextInSquence()
        {
            try
            {
                var url = string.Format("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v1?key={0}&matches_requested=1&game_mode=18", _siteSettings.Value.DotaApiKey);

                var httpClient = new HttpClient();
                var jsonData = httpClient.GetStringAsync(url).Result;
                var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.DotaApi.GetMatchHistory.Root>(jsonData);

                return root.result.matches
                    .Select(_ => Tuple.Create(_.match_id, _.match_seq_num))
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return _dbContext.Checkpoints
                    .OrderByDescending(_ => _.MatchNumber)
                    .Select(_ => Tuple.Create(0L, _.MatchNumber + 1))
                    .FirstOrDefault();
            }
        }

        private void ProcessMatches(long next)
        {
            var url = string.Format("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v1?key={0}&start_at_match_seq_num={1}", _siteSettings.Value.DotaApiKey, next);

            var httpClient = new HttpClient();
            var jsonData = httpClient.GetStringAsync(url).Result;
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.DotaApi.GetMatchHistoryBySequenceNum.Root>(jsonData);

            var matches = root.result.matches
                .Where(_ => _.game_mode == 18)
                .Where(_ => _.players != null)
                .Where(_ => _.players.Count == 10)
                .ToList();

            foreach (var m in matches)
            {
                var checkpoint = _dbContext.Checkpoints.FirstOrDefault(_ => _.MatchId == m.match_id);
                if (checkpoint != null)
                {
                    _logger.LogDebug("Already Processed Match: {0}", m.match_id);
                    continue;
                }

                foreach (var p in m.players)
                {
                    var hero = _dbContext.NextHeroTrends.FirstOrDefault(_ => _.HeroId == p.hero_id);
                    if (hero == null)
                    {
                        _logger.LogDebug("Can not find hero: {0} {1}", m.match_id, p.hero_id);
                        continue;
                    }

                    var score = m.radiant_win == true ? (p.player_slot < 5) : (p.player_slot > 5);
                    var win = score == true ? 1 : 0;
                    var loss = score == false ? 1 : 0;

                    hero.Wins += win;
                    hero.Loses += loss;
                    hero.Kills += p.kills;
                    hero.Deaths += p.deaths;
                    hero.Assists += p.assists;
                    hero.Total++;

                    var abilities = p.ability_upgrades.Select(_ => _.ability).Where(_ => _ != 5002).Distinct().ToList();
                    foreach (var id in abilities)
                    {
                        var heroCombo = _dbContext.NextHeroComboTrends.FirstOrDefault(_ => _.HeroId == p.hero_id && _.AbilityId == id);
                        if (heroCombo == null)
                        {
                            _logger.LogDebug("Can not find hero combo: {0} {1} {2}", m.match_id, p.hero_id, id);
                            continue;
                        }

                        heroCombo.Wins += win;
                        heroCombo.Loses += loss;
                        heroCombo.Kills += p.kills;
                        heroCombo.Deaths += p.deaths;
                        heroCombo.Assists += p.assists;
                        heroCombo.Total++;

                        var ability = _dbContext.NextAbilityTrends.FirstOrDefault(_ => _.AbilityId == id);
                        if (ability == null)
                        {
                            _logger.LogDebug("Can not find ability: {0} {1}", m.match_id, id);
                            continue;
                        }

                        ability.Wins += win;
                        ability.Loses += loss;
                        ability.Kills += p.kills;
                        ability.Deaths += p.deaths;
                        ability.Assists += p.assists;
                        ability.Total++;
                    }

                    // Cross Join
                    var abilityCombos = abilities
                        .SelectMany(x => abilities, (x, y) => Tuple.Create(x, y))
                        .Where(_ => _.Item1 != _.Item2)
                        .ToList();

                    foreach (var combo in abilityCombos)
                    {
                        var abilityCombo = _dbContext.NextAbilityComboTrends.FirstOrDefault(_ => _.AbilityId == combo.Item1 && _.ComboId == combo.Item2);
                        if (abilityCombo == null)
                        {
                            _logger.LogDebug("Can not find ability combo: {0} {1} {2}", m.match_id, combo.Item1, combo.Item2);
                            continue;
                        }

                        abilityCombo.Wins += win;
                        abilityCombo.Loses += loss;
                        abilityCombo.Kills += p.kills;
                        abilityCombo.Deaths += p.deaths;
                        abilityCombo.Assists += p.assists;
                        abilityCombo.Total++;
                    }
                }

                _dbContext.Checkpoints.Add(new Checkpoint()
                {
                    MatchId = m.match_id,
                    MatchNumber = m.match_seq_num,
                    MatchDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddSeconds(m.start_time)
                });
                _dbContext.SaveChanges();
            }
        }
    }
}