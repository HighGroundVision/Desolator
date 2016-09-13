using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.DotaApi.GetMatchHistoryBySequenceNum
{
    public class Root
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public List<Match> matches { get; set; }

        public int status { get; set; }

        public Result()
        {
            this.matches = new List<Match>();
        }
    }

    public class Match
    {
        public long match_id { get; set; }
        public long match_seq_num { get; set; }

        public bool radiant_win { get; set; }
        public int duration { get; set; }
        public long start_time { get; set; }
        public int tower_status_radiant { get; set; }
        public int tower_status_dire { get; set; }
        public int barracks_status_radiant { get; set; }
        public int barracks_status_dire { get; set; }
        public int cluster { get; set; }
        public int first_blood_time { get; set; }
        public int lobby_type { get; set; }
        public int human_players { get; set; }
        public int leagueid { get; set; }
        public int positive_votes { get; set; }
        public int negative_votes { get; set; }
        public int game_mode { get; set; }
        public int engine { get; set; }

        public List<Player> players { get; set; }

        public Match()
        {
            this.players = new List<Player>();
        }
    }

    public class Player
    {
        public int player_slot { get; set; }
        public long account_id { get; set; }
        public int hero_id { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int last_hits { get; set; }
        public int denies { get; set; }
        public int gold { get; set; }
        public int level { get; set; }
        public int gold_per_min { get; set; }
        public int xp_per_min { get; set; }
        public int item0 { get; set; }
        public int item1 { get; set; }
        public int item2 { get; set; }
        public int item3 { get; set; }
        public int item4 { get; set; }
        public int item5 { get; set; }
        public int gold_spent { get; set; }
        public int hero_damage { get; set; }
        public int tower_damage { get; set; }
        public int hero_healing { get; set; }

        public List<AbilityUpgrade> ability_upgrades { get; set; }

        public Player()
        {
            this.ability_upgrades = new List<AbilityUpgrade>();
        }
    }

    public class AbilityUpgrade
    {
        public int ability { get; set; }
        public int time { get; set; }
        public int level { get; set; }
    }
}
