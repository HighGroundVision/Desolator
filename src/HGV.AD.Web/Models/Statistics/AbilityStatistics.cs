using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Statistics
{
    public class PerviousAbilityStat : AbilityStatBase
	{
    }

	public class CurrentAbilityStat : AbilityStatBase
	{
        public static AbilityStatBase operator -(CurrentAbilityStat c1, PerviousAbilityStat c2) =>
            new AbilityStatBase()
            {
                Id = 0,
                AbilityId = c1.AbilityId,
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

	public class NextAbilityStat : AbilityStatBase
	{
	}

	public class AbilityStatBase
	{
		public int Id { get; set; }

		public int AbilityId { get; set; }
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
