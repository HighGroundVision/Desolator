using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HGV.AD.Web.Models;
using HGV.AD.Web.Models.Attributes;
using HGV.AD.Web.Models.Trends;
using HGV.AD.Web.Models.Statistics;

namespace HGV.AD.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		
		// Heroes
		public DbSet<HeroAttributes> Heroes { get; set; }
        public DbSet<HeroAttributeRank> HeroAttributeRanks { get; set; }

		// Heroes' abilities
		public DbSet<AbilityAttributes> Abilities { get; set; }
        public DbSet<TalenetAttributes> Talenets { get; set; }

        // Statistics
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<Batch> Batchs { get; set; }

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

            builder.Entity<Checkpoint>().HasKey(_ => _.Id);

            builder.Entity<HeroAttributes>().HasKey(_ => _.HeroId);
            builder.Entity<HeroAttributes>().Property(_ => _.HeroId).ValueGeneratedNever();

            builder.Entity<HeroAttributeRank>().HasKey(_ => _.Id);

            builder.Entity<AbilityAttributes>().HasKey(_ => _.AbilityId);
            builder.Entity<AbilityAttributes>().Property(_ => _.AbilityId).ValueGeneratedNever();

            builder.Entity<TalenetAttributes>().HasKey(_ => new { _.HeroId, _.AbilityId });

            builder.Entity<PerviousAbilityStat>().HasKey(_ => _.AbilityId);
            builder.Entity<PerviousAbilityStat>().Property(_ => _.AbilityId).ValueGeneratedNever();
            builder.Entity<CurrentAbilityStat>().HasKey(_ => _.AbilityId);
            builder.Entity<CurrentAbilityStat>().Property(_ => _.AbilityId).ValueGeneratedNever();
            builder.Entity<NextAbilityStat>().HasKey(_ => _.AbilityId);
            builder.Entity<NextAbilityStat>().Property(_ => _.AbilityId).ValueGeneratedNever();

            builder.Entity<PerviousHeroStat>().HasKey(_ => _.HeroId);
            builder.Entity<PerviousHeroStat>().Property(_ => _.HeroId).ValueGeneratedNever();
            builder.Entity<CurrentHeroStat>().HasKey(_ => _.HeroId);
            builder.Entity<CurrentHeroStat>().Property(_ => _.HeroId).ValueGeneratedNever();
            builder.Entity<NextHeroStat>().HasKey(_ => _.HeroId);
            builder.Entity<NextHeroStat>().Property(_ => _.HeroId).ValueGeneratedNever();

            builder.Entity<PerviousHeroComboStats>().HasKey(_ => new { _.HeroId, _.AbilityId });
            builder.Entity<CurrentHeroComboStats>().HasKey(_ => new { _.HeroId, _.AbilityId });
            builder.Entity<NextHeroComboStats>().HasKey(_ => new { _.HeroId, _.AbilityId });

            builder.Entity<PerviousAbilityComboStat>().HasKey(_ => new { _.AbilityId, _.ComboId });
            builder.Entity<CurrentAbilityComboStat>().HasKey(_ => new { _.AbilityId, _.ComboId });
            builder.Entity<NextAbilityComboStat>().HasKey(_ => new { _.AbilityId, _.ComboId });
        }
    }
}
