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
            // There should always be one and only one
            var lastCheckpoint = _dbContext.Checkpoints.FirstOrDefault(); 
            if (lastCheckpoint == null)
                throw new NullReferenceException("Can not find last checkpoint");

            var from = lastCheckpoint.MatchNumber;

            var latestMatch = GetNextMatch();
            var to = latestMatch.match_seq_num;

            var batchSize = (to - from);

            if (batchSize < 5)
                throw new ArgumentOutOfRangeException(string.Format("Batch size of {0} is to small to processs.", batchSize));

            lastCheckpoint.MatchNumber = latestMatch.match_seq_num;
            lastCheckpoint.MatchId = latestMatch.match_id;
            lastCheckpoint.MatchDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(latestMatch.start_time);

            _dbContext.SaveChanges();

            if (batchSize > 100000)
                throw new ArgumentOutOfRangeException(string.Format("Batch size of {0} is to big to processs.", batchSize));

            ProcessBatch(from, to);
        }

        private Models.DotaApi.GetMatchHistory.Match GetNextMatch()
        {
            var url = string.Format("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v1?key={0}&matches_requested=1&game_mode=18", _siteSettings.Value.DotaApiKey);

            var httpClient = new HttpClient();
            var jsonData = httpClient.GetStringAsync(url).Result;
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.DotaApi.GetMatchHistory.Root>(jsonData);

            return root.result.matches.OrderByDescending(_ => _.match_seq_num).FirstOrDefault();
        }

        private void ProcessBatch(long frist, long last)
        {
            var current = frist;
            while(current < last)
            {
                try
                {
                    current = ProcessMatches(current);

                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                catch (Exception ex)
                {
                    this._logger.LogDebug(ex.Message);

                    Thread.Sleep(TimeSpan.FromSeconds(30));
                }
            }
        }

        private long ProcessMatches(long next)
        {
            var url = string.Format("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v1?key={0}&start_at_match_seq_num={1}", _siteSettings.Value.DotaApiKey, next);

            var httpClient = new HttpClient();
            var jsonData = httpClient.GetStringAsync(url).Result;
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.DotaApi.GetMatchHistoryBySequenceNum.Root>(jsonData);

            var matches = root.result.matches
                .Where(_ => _.game_mode == 18)          // Is the game mode Ability Drafter
                .Where(_ => _.duration > 300)           // Is the duration greater then 5 mins
                .Where(_ => _.players != null)          // Are thre players
                .Where(_ => _.players.Count == 10)      // Are there 10 players
                .ToList();

            foreach (var m in matches)
            {
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

                    foreach (var pair in abilityCombos)
                    {
                        var abilityCombo = _dbContext.NextAbilityComboTrends.FirstOrDefault(_ => _.AbilityId == pair.Item1 && _.ComboId == pair.Item2);
                        if (abilityCombo == null)
                        {
                            _logger.LogDebug("Can not find ability combo: {0} {1} {2}", m.match_id, pair.Item1, pair.Item2);
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

                _dbContext.SaveChanges();
            }

            return root.result.matches.Max(_ => _.match_seq_num);
        }
    }
}