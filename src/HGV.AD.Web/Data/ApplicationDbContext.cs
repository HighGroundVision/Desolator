using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HGV.AD.Web.Models;
using HGV.AD.Web.Models.Attributes;
using HGV.AD.Web.Models.Statistics;
using HGV.AD.Web.Models.Checkpoints;

namespace HGV.AD.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		
		// Heroes
		public DbSet<HeroAttributes> Heroes { get; set; }

        public DbSet<HeroAttributeRank> HeroAttributeRanks { get; set; }

		// Heroes' abilities
		public DbSet<AbilityAttributes> Abilities { get; set; }

        public DbSet<Checkpoint> Checkpoints { get; set; }

        // Trends - abilities
        public DbSet<PerviousAbilityStat> PerviousAbilityTrends { get; set; }

		public DbSet<CurrentAbilityStat> CurrentAbilityTrends { get; set; }

		public DbSet<NextAbilityStat> NextAbilityTrends { get; set; }

		// Trends - abilities combos (2 abilities)
		public DbSet<PerviousAbilityComboStat> PerviousAbilityComboTrends { get; set; }

		public DbSet<CurrentAbilityComboStat> CurrentAbilityComboTrends { get; set; }

		public DbSet<NextAbilityComboStat> NextAbilityComboTrends { get; set; }

		// Trends - heroes
		public DbSet<PerviousHeroStat> PerviousHeroTrends { get; set; }

		public DbSet<CurrentHeroStat> CurrentHeroTrends { get; set; }

		public DbSet<NextHeroStat> NextHeroTrends { get; set; }

		// Trends - hero combos (1 hero / 1 abilities)
		public DbSet<PerviousHeroComboStats> PerviousHeroComboTrends { get; set; }

		public DbSet<CurrentHeroComboStats> CurrentHeroComboTrends { get; set; }

		public DbSet<NextHeroComboStats> NextHeroComboTrends { get; set; }
		

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PerviousAbilityStat>().HasIndex(_ => _.AbilityId);
            builder.Entity<CurrentAbilityStat>().HasIndex(_ => _.AbilityId);
            builder.Entity<NextAbilityStat>().HasIndex(_ => _.AbilityId);

            builder.Entity<PerviousAbilityComboStat>().HasIndex(_ => _.AbilityId);
            builder.Entity<PerviousAbilityComboStat>().HasIndex(_ => _.ComboId);
            builder.Entity<CurrentAbilityComboStat>().HasIndex(_ => _.AbilityId);
            builder.Entity<CurrentAbilityComboStat>().HasIndex(_ => _.ComboId);
            builder.Entity<NextAbilityComboStat>().HasIndex(_ => _.AbilityId);
            builder.Entity<NextAbilityComboStat>().HasIndex(_ => _.ComboId);

            builder.Entity<PerviousHeroStat>().HasIndex(_ => _.HeroId);
            builder.Entity<CurrentHeroStat>().HasIndex(_ => _.HeroId);
            builder.Entity<NextHeroStat>().HasIndex(_ => _.HeroId);

            builder.Entity<PerviousHeroComboStats>().HasIndex(_ => _.HeroId);
            builder.Entity<PerviousHeroComboStats>().HasIndex(_ => _.AbilityId);
            builder.Entity<CurrentHeroComboStats>().HasIndex(_ => _.HeroId);
            builder.Entity<CurrentHeroComboStats>().HasIndex(_ => _.AbilityId);
            builder.Entity<NextHeroComboStats>().HasIndex(_ => _.HeroId);
            builder.Entity<NextHeroComboStats>().HasIndex(_ => _.AbilityId);
        }
    }
}
