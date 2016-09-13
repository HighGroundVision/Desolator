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
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Key]
		public int Id { get; set; }
        public int LookupId { get; set; }
        public string Identity { get; set; }
		public string Name { get; set; }
        public string Patch { get; set; }
        public int Primary { get; set; }
        public int Alignment { get; set; }

        [Display(Name = "Movespeed")]
        public double Movespeed { get; set; }

        [Display(Name = "Max Dmg")]
        public double MaxDmg { get; set; }

        [Display(Name = "Min Dmg")]
        public double MinDmg { get; set; }

        [Display(Name = "HP")]
        public double HP { get; set; }

        [Display(Name = "Mana")]
        public double Mana { get; set; }

        [Display(Name = "HP Regen")]
        public double HPRegen { get; set; }

        [Display(Name = "Mana Regen")]
        public double ManaRegen { get; set; }

        [Display(Name = "Armor")]
        public double Armor { get; set; }

        [Display(Name = "Range")]
        public double Range { get; set; }

        [Display(Name = "Projectile Speed")]
        public double ProjectileSpeed { get; set; }

        [Display(Name = "Base Str")]
        public double BaseStr { get; set; }

        [Display(Name = "Base Agi")]
        public double BaseAgi { get; set; }

        [Display(Name = "Base Int")]
        public double BaseInt { get; set; }

        [Display(Name = "Str Gain")]
        public double StrGain { get; set; }

        [Display(Name = "Agi Gain")]
        public double AgiGain { get; set; }

        [Display(Name = "Int Gain")]
        public double IntGain { get; set; }

        [Display(Name = "Base Attack Time")]
        public double BaseAttackTime { get; set; }

        [Display(Name = "Day Vision")]
        public double DayVision { get; set; }

        [Display(Name = "Night Vision")]
        public double NightVision { get; set; }

        [Display(Name = "Attack Point")]
        public double AttackPoint { get; set; }

        [Display(Name = "Attack Swing")]
        public double AttackSwing { get; set; }

        [Display(Name = "Cast Point")]
        public double CastPoint { get; set; }

        [Display(Name = "Cast Swing")]
        public double CastSwing { get; set; }

        [Display(Name = "Turnrate")]
        public double Turnrate { get; set; }

        public string GetPrimary()
        {
            switch (this.Primary)
            {
                case 0:
                    return "Strength";
                case 1:
                    return "Agility";
                case 2:
                    return "Intelligence";
                default:
                    return string.Empty;
            }
        }

        public string GetAlignment()
        {
            switch (this.Primary)
            {
                case 0:
                    return "Dire";
                case 1:
                    return "Radiant";
                default:
                    return string.Empty;
            }
        }
    }
}
