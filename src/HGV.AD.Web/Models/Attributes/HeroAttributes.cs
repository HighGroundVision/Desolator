using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Attributes
{
    public class HeroAttributes
	{
		public int HeroId { get; set; }
        public string Identity { get; set; }
		public string Name { get; set; }
        public string Patch { get; set; }
        public string Primary { get; set; }

        public double Movespeed { get; set; }

        public double MaxDmg { get; set; }

        public double MinDmg { get; set; }

        public double AvgDmg { get; set; }

        public double HP { get; set; }

        public double Mana { get; set; }

        public double HPRegen { get; set; }

        public double ManaRegen { get; set; }

        public double Armor { get; set; }

        public double Range { get; set; }

        public double ProjectileSpeed { get; set; }

        public double BaseStr { get; set; }

        public double BaseAgi { get; set; }

        public double BaseInt { get; set; }

        public double StrGain { get; set; }

        public double AgiGain { get; set; }

        public double IntGain { get; set; }

        public double DayVision { get; set; }

        public double NightVision { get; set; }

        public double Turnrate { get; set; }
    }
}
