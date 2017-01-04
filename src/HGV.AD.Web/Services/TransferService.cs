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
using HGV.AD.Web.Models.Trends;

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
            _dbContext.Database.SetCommandTimeout(300);
            _logger = loggerFactory.CreateLogger<TransferService>();
        }

        public void Transfer()
        {
            HeroTrends();
            HeroComboTrends();

            AbilityTrends();
            AbilityComboTrends();
        }

        private void HeroTrends()
        {
            var sql = @"
update _previous
set _previous.[Assists] = _current.[Assists]
,_previous.[Deaths] = _current.[Deaths]
,_previous.[Kills] = _current.[Kills]
,_previous.[Loses] = _current.[Loses]
,_previous.[Total] = _current.[Total]
,_previous.[Wins] = _current.[Wins]
from CurrentHeroTrends _current
    join PerviousHeroTrends _previous 
	on _current.HeroId = _previous.HeroId

update _current
set _current.[Assists] = _next.[Assists]
,_current.[Deaths] = _next.[Deaths]
,_current.[Kills] = _next.[Kills]
,_current.[Loses] = _next.[Loses]
,_current.[Total] = _next.[Total]
,_current.[Wins] = _next.[Wins]
from NextHeroTrends _next
    join CurrentHeroTrends _current 
	on _next.HeroId = _current.HeroId
	
UPDATE NextHeroTrends
SET [Assists] = 0, [Deaths] = 0, [Kills] = 0, [Loses] = 0, [Total] = 0, [Wins] = 0
";
            _dbContext.Database.ExecuteSqlCommand(sql);
        }

        private void AbilityTrends()
        {
            var sql = @"
update _previous
set _previous.[Assists] = _current.[Assists]
,_previous.[Deaths] = _current.[Deaths]
,_previous.[Kills] = _current.[Kills]
,_previous.[Loses] = _current.[Loses]
,_previous.[Total] = _current.[Total]
,_previous.[Wins] = _current.[Wins]
from CurrentAbilityTrends _current
    join PerviousAbilityTrends _previous 
	on _current.AbilityId = _previous.AbilityId

update _current
set _current.[Assists] = _next.[Assists]
,_current.[Deaths] = _next.[Deaths]
,_current.[Kills] = _next.[Kills]
,_current.[Loses] = _next.[Loses]
,_current.[Total] = _next.[Total]
,_current.[Wins] = _next.[Wins]
from NextAbilityTrends _next
    join CurrentAbilityTrends _current 
	on _next.AbilityId = _current.AbilityId
	
UPDATE NextAbilityTrends
SET [Assists] = 0, [Deaths] = 0, [Kills] = 0, [Loses] = 0, [Total] = 0, [Wins] = 0
";
            _dbContext.Database.ExecuteSqlCommand(sql);
        }

        private void AbilityComboTrends()
        {
            var sql = @"
update _previous
set _previous.[Assists] = _current.[Assists]
,_previous.[Deaths] = _current.[Deaths]
,_previous.[Kills] = _current.[Kills]
,_previous.[Loses] = _current.[Loses]
,_previous.[Total] = _current.[Total]
,_previous.[Wins] = _current.[Wins]
from CurrentAbilityComboTrends _current
    join [PerviousAbilityComboTrends] _previous 
	on _current.AbilityId = _previous.AbilityId and _current.ComboId = _previous.ComboId

update _current
set _current.[Assists] = _next.[Assists]
,_current.[Deaths] = _next.[Deaths]
,_current.[Kills] = _next.[Kills]
,_current.[Loses] = _next.[Loses]
,_current.[Total] = _next.[Total]
,_current.[Wins] = _next.[Wins]
from NextAbilityComboTrends _next
    join [CurrentAbilityComboTrends] _current 
	on _next.AbilityId = _current.AbilityId and _next.ComboId = _current.ComboId

UPDATE [NextAbilityComboTrends]
SET [Assists] = 0, [Deaths] = 0, [Kills] = 0, [Loses] = 0, [Total] = 0, [Wins] = 0
";
            _dbContext.Database.ExecuteSqlCommand(sql);
        }

        private void HeroComboTrends()
        {
            var sql = @"
update _previous
set _previous.[Assists] = _current.[Assists]
,_previous.[Deaths] = _current.[Deaths]
,_previous.[Kills] = _current.[Kills]
,_previous.[Loses] = _current.[Loses]
,_previous.[Total] = _current.[Total]
,_previous.[Wins] = _current.[Wins]
from CurrentHeroComboTrends _current
    join PerviousHeroComboTrends _previous 
	on _current.HeroId = _previous.HeroId and _current.AbilityId = _previous.AbilityId

update _current
set _current.[Assists] = _next.[Assists]
,_current.[Deaths] = _next.[Deaths]
,_current.[Kills] = _next.[Kills]
,_current.[Loses] = _next.[Loses]
,_current.[Total] = _next.[Total]
,_current.[Wins] = _next.[Wins]
from NextHeroComboTrends _next
    join CurrentHeroComboTrends _current 
	on _next.HeroId = _current.HeroId and _next.AbilityId = _current.AbilityId
	
UPDATE NextHeroComboTrends
SET [Assists] = 0, [Deaths] = 0, [Kills] = 0, [Loses] = 0, [Total] = 0, [Wins] = 0
";
            _dbContext.Database.ExecuteSqlCommand(sql);
        }
    }
}
