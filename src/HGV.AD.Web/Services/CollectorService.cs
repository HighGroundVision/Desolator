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
using Hangfire.Server;
using Hangfire.Console;

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
            _logger = loggerFactory.CreateLogger<CollectorService>();
            _dbContext = dbcontext;
            _siteSettings = siteSettings;
        }

        public void CalucateBatch()
        {
            // There should always be one and only one
            var lastCheckpoint = _dbContext.Checkpoints.FirstOrDefault();
            if (lastCheckpoint == null)
                throw new NullReferenceException("Can not find last checkpoint");

            var latestMatch = GetNextMatch();

            var from = lastCheckpoint.MatchNumber;
            var to = latestMatch.match_seq_num;

            lastCheckpoint.MatchNumber = latestMatch.match_seq_num;
            lastCheckpoint.MatchId = latestMatch.match_id;
            lastCheckpoint.MatchDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(latestMatch.start_time);

            _dbContext.SaveChanges();

            var batchSize = (to - from);
            if (batchSize > 10000)
                from = to - 10000; // Limit batch size to 10,000

            BackgroundJob.Enqueue<CollectorService>(_ => _.ProcessBatch(null, from, to));
        }

        public void ProcessBatch(PerformContext context, long frist, long last)
        {
            this.Log(context, string.Format("Delta: ~{0}", (last - frist)));

            var matches = ExtractMatchesFromBatch(context, frist, last);

            this.Log(context, string.Format("AD Matches: {0}", matches.Count));

            ProcessMatches(context, matches);

            this.Log(context, "Saving Results");

            _dbContext.SaveChanges();
        }

        private void Log(PerformContext context, string msg, ConsoleTextColor color = null)
        {
            if(color != null) context.SetTextColor(color);

            context.WriteLine(msg);

            if (color != null) context.ResetTextColor();
        }

        private Models.DotaApi.GetMatchHistory.Match GetNextMatch()
        {
            var url = string.Format("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v1?key={0}&matches_requested=1", _siteSettings.Value.DotaApiKey);

            var httpClient = new HttpClient();
            var jsonData = httpClient.GetStringAsync(url).Result;
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.DotaApi.GetMatchHistory.Root>(jsonData);

            return root.result.matches.OrderByDescending(_ => _.match_seq_num).FirstOrDefault();
        }

        private List<Models.DotaApi.GetMatchHistoryBySequenceNum.Match> ExtractMatchesFromBatch(PerformContext context, long frist, long last)
        {
            var progress = context.WriteProgressBar();

            var matches = new List<Models.DotaApi.GetMatchHistoryBySequenceNum.Match>();

            var circuitBreaker = 0;

            var current = frist;
            while (current < last)
            {
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));

                    progress.SetValue((((current - frist) / (double)(last - frist)) * 100));

                    var newMatches = GetMatches(current, out current);
                    matches.AddRange(newMatches);

                    circuitBreaker = 0;
                }
                catch (ApplicationException ex)
                {
                    circuitBreaker++;
                    
                    this.Log(context, ex.Message, ConsoleTextColor.Red);

                    if (circuitBreaker > 3)
                        throw ex;

                    Thread.Sleep(TimeSpan.FromSeconds(30));
                }
            }

            progress.SetValue(100);

            return matches;
        }

        private List<Models.DotaApi.GetMatchHistoryBySequenceNum.Match> GetMatches(long current, out long next)
        {
            try
            {
                var url = string.Format("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v1?key={0}&start_at_match_seq_num={1}", _siteSettings.Value.DotaApiKey, current);

                var httpClient = new HttpClient();
                var jsonData = httpClient.GetStringAsync(url).Result;
                var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.DotaApi.GetMatchHistoryBySequenceNum.Root>(jsonData);

                next = root.result.matches.Max(_ => _.match_seq_num);

                var matches = root.result.matches
                    .Where(_ => _.game_mode == 18)          // Is the game mode Ability Drafter
                    .Where(_ => _.duration > 300)           // Is the duration greater then 5 minutes
                    .Where(_ => _.players != null)          // Are there players
                    .Where(_ => _.players.Count == 10)      // Are there 10 players
                    .ToList();

                return matches;
            }
            catch (AggregateException ex)
            {
                throw new ApplicationException("Dota API interruption.", ex);
            }
        }

        private void ProcessMatches(PerformContext context, List<Models.DotaApi.GetMatchHistoryBySequenceNum.Match> matches)
        {
            var progress = context.WriteProgressBar();

            foreach (var m in matches.WithProgress(progress))
            {
                foreach (var p in m.players)
                {
                    var hero = _dbContext.NextHeroTrends.FirstOrDefault(_ => _.HeroId == p.hero_id);
                    if (hero == null)
                    {
                        this.Log(context, string.Format("Can not find hero: {0} {1}", m.match_id, p.hero_id), ConsoleTextColor.Red);
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

                    var abilities = p.ability_upgrades
                        .Select(_ => _.ability)
                        .Distinct()
                        .ToList();

                    var heroCollection = _dbContext.NextHeroComboTrends
                        .Where(_ => p.hero_id == _.HeroId)
                        .Where(_ => abilities.Contains(_.AbilityId) == true)
                        .ToList();

                    foreach (var item in heroCollection)
                    {
                        item.Wins += win;
                        item.Loses += loss;
                        item.Kills += p.kills;
                        item.Deaths += p.deaths;
                        item.Assists += p.assists;
                        item.Total++;
                    }

                    var abilityCollection = _dbContext.NextAbilityTrends
                        .Where(_ => abilities.Contains(_.AbilityId) == true)
                        .ToList();

                    foreach (var item in abilityCollection)
                    {
                        item.Wins += win;
                        item.Loses += loss;
                        item.Kills += p.kills;
                        item.Deaths += p.deaths;
                        item.Assists += p.assists;
                        item.Total++;
                    }

                    var comboCollection = _dbContext.NextAbilityComboTrends
                        .Where(_ => abilities.Contains(_.AbilityId))
                        .Where(_ => abilities.Contains(_.ComboId))
                        .Where(_ => _.AbilityId != _.ComboId)
                        .ToList();

                    foreach (var item in comboCollection)
                    {
                        item.Wins += win;
                        item.Loses += loss;
                        item.Kills += p.kills;
                        item.Deaths += p.deaths;
                        item.Assists += p.assists;
                        item.Total++;
                    }
                }
            }
        }
        
    }
}