using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Statistics
{
    public class PerviousHeroStat : HeroStatBase
	{
    }

	public class CurrentHeroStat: HeroStatBase
	{
        public static HeroStatBase operator -(CurrentHeroStat c1, PerviousHeroStat c2) =>
           new HeroStatBase()
           {
               HeroId = c1.HeroId,
               Name = c1.Name,
               Identity = c1.Identity,
               Wins = c1.Wins - c2.Wins,
               Loses = c1.Loses - c2.Loses,
               Kills = c1.Kills - c2.Kills,
               Deaths = c1.Deaths - c2.Deaths,
               Assists = c1.Assists - c2.Assists,
               Total = c1.Total - c2.Total,
           };
    }

	public class NextHeroStat : HeroStatBase
	{
	}

	public class HeroStatBase
    {
        public int HeroId { get; set; }
        public string Identity { get; set; }
        public string Name { get; set; }

        public long Wins { get; set; }
		public long Loses { get; set; }
		public long Kills { get; set; }
		public long Deaths { get; set; }
		public long Assists { get; set; }
		public long Total { get; set; }
	}
}
