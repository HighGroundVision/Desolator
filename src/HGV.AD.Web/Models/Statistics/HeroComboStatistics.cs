using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Statistics
{
    public class PerviousHeroComboStats : HeroComboStatsBase
	{
    }

	public class CurrentHeroComboStats : HeroComboStatsBase
	{
        public static HeroComboStatsBase operator -(CurrentHeroComboStats c1, PerviousHeroComboStats c2) =>
            new HeroComboStatsBase()
            {
                AbilityId = c1.AbilityId,
                AbilityName = c1.AbilityName,
                AbilityIdentity = c1.AbilityIdentity,
                HeroId = c1.HeroId,
                HeroName = c1.HeroName,
                HeroIdentity = c1.HeroIdentity,
                Wins = c1.Wins - c2.Wins,
                Loses = c1.Loses - c2.Loses,
                Kills = c1.Kills - c2.Kills,
                Deaths = c1.Deaths - c2.Deaths,
                Assists = c1.Assists - c2.Assists,
                Total = c1.Total - c2.Total,
            };
    }

	public class NextHeroComboStats : HeroComboStatsBase
	{
	}

	public class HeroComboStatsBase
	{
        public int HeroId { get; set; }
        public string HeroIdentity { get; set; }
        public string HeroName { get; set; }

        public int AbilityId { get; set; }
        public string AbilityIdentity { get; set; }
        public string AbilityName { get; set; }

        public long Wins { get; set; }
		public long Loses { get; set; }
		public long Kills { get; set; }
		public long Deaths { get; set; }
		public long Assists { get; set; }
		public long Total { get; set; }
	}
}
