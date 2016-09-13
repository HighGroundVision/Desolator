using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Statistics
{
    public class PerviousAbilityComboStat : AbilityComboStatBase
	{
    }

	public class CurrentAbilityComboStat : AbilityComboStatBase
	{
        public static AbilityComboStatBase operator -(CurrentAbilityComboStat c1, PerviousAbilityComboStat c2) =>
            new AbilityComboStatBase()
            {
                Id = 0,
                AbilityId = c1.AbilityId,
                AbilityIdentity = c1.AbilityIdentity,
                AbilityName = c1.AbilityName,
                ComboId = c1.ComboId,
                ComboIdentity = c1.ComboIdentity,
                ComboName = c1.ComboName,
                Wins = c1.Wins - c2.Wins,
                Loses = c1.Loses - c2.Loses,
                Kills = c1.Kills - c2.Kills,
                Deaths = c1.Deaths - c2.Deaths,
                Assists = c1.Assists - c2.Assists,
                Total = c1.Total - c2.Total,
            };
    }

	public class NextAbilityComboStat : AbilityComboStatBase
	{
	}

	public class AbilityComboStatBase
	{
		public long Id { get; set; }

		public int AbilityId { get; set; }
        public string AbilityIdentity { get; set; }
        public string AbilityName { get; set; }

        public int ComboId { get; set; }
        public string ComboIdentity { get; set; }
        public string ComboName { get; set; }

        public long Wins { get; set; }
		public long Loses { get; set; }
		public long Kills { get; set; }
		public long Deaths { get; set; }
		public long Assists { get; set; }
		public long Total { get; set; }
	}
}
