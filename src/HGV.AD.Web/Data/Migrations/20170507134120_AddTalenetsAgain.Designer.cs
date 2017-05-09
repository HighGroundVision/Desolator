using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HGV.AD.Web.Data;

namespace HGV.AD.Web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170507134120_AddTalenetsAgain")]
    partial class AddTalenetsAgain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HGV.AD.Web.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Attributes.AbilityAttributes", b =>
                {
                    b.Property<int>("AbilityId");

                    b.Property<string>("AffectsWhom");

                    b.Property<string>("DamageType");

                    b.Property<string>("Description");

                    b.Property<int>("HeroId");

                    b.Property<string>("Html");

                    b.Property<string>("Identity");

                    b.Property<string>("Keywords");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("Target");

                    b.Property<bool>("Ultimate");

                    b.HasKey("AbilityId");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Attributes.HeroAttributeRank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Avg");

                    b.Property<double>("Groups");

                    b.Property<int>("HeroId");

                    b.Property<int>("Index");

                    b.Property<double>("Max");

                    b.Property<double>("Min");

                    b.Property<string>("Name");

                    b.Property<double>("Percentage");

                    b.Property<int>("Rank");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.ToTable("HeroAttributeRanks");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Attributes.HeroAttributes", b =>
                {
                    b.Property<int>("HeroId");

                    b.Property<double>("AgiGain");

                    b.Property<double>("Armor");

                    b.Property<double>("AvgDmg");

                    b.Property<double>("BaseAgi");

                    b.Property<double>("BaseInt");

                    b.Property<double>("BaseStr");

                    b.Property<double>("DayVision");

                    b.Property<double>("HP");

                    b.Property<double>("HPRegen");

                    b.Property<string>("Identity");

                    b.Property<double>("IntGain");

                    b.Property<double>("Mana");

                    b.Property<double>("ManaRegen");

                    b.Property<double>("MaxDmg");

                    b.Property<double>("MinDmg");

                    b.Property<double>("Movespeed");

                    b.Property<string>("Name");

                    b.Property<double>("NightVision");

                    b.Property<string>("Notes");

                    b.Property<string>("Patch");

                    b.Property<string>("Primary");

                    b.Property<double>("ProjectileSpeed");

                    b.Property<double>("Range");

                    b.Property<double>("StrGain");

                    b.Property<double>("Turnrate");

                    b.HasKey("HeroId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Attributes.TalenetAttributes", b =>
                {
                    b.Property<int>("HeroId");

                    b.Property<int>("AbilityId");

                    b.Property<string>("Key");

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.HasKey("HeroId", "AbilityId");

                    b.ToTable("Talenets");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Statistics.Batch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateProcessed");

                    b.Property<int>("NumberOfAD");

                    b.Property<int>("NumberOfMatches");

                    b.Property<long>("SquenceEnd");

                    b.Property<long>("SquenceStart");

                    b.HasKey("Id");

                    b.ToTable("Batchs");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Statistics.Checkpoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("MatchDate");

                    b.Property<long>("MatchId");

                    b.Property<long>("MatchNumber");

                    b.HasKey("Id");

                    b.ToTable("Checkpoints");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.CurrentAbilityComboStat", b =>
                {
                    b.Property<int>("AbilityId");

                    b.Property<int>("ComboId");

                    b.Property<string>("AbilityIdentity");

                    b.Property<string>("AbilityName");

                    b.Property<long>("Assists");

                    b.Property<string>("ComboIdentity");

                    b.Property<string>("ComboName");

                    b.Property<long>("Deaths");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<bool>("SameSource");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("AbilityId", "ComboId");

                    b.ToTable("CurrentAbilityComboTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.CurrentAbilityStat", b =>
                {
                    b.Property<int>("AbilityId");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("Identity");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<string>("Name");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("AbilityId");

                    b.ToTable("CurrentAbilityTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.CurrentHeroComboStats", b =>
                {
                    b.Property<int>("HeroId");

                    b.Property<int>("AbilityId");

                    b.Property<string>("AbilityIdentity");

                    b.Property<string>("AbilityName");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("HeroIdentity");

                    b.Property<string>("HeroName");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<bool>("SameSource");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("HeroId", "AbilityId");

                    b.ToTable("CurrentHeroComboTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.CurrentHeroStat", b =>
                {
                    b.Property<int>("HeroId");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("Identity");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<string>("Name");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("HeroId");

                    b.ToTable("CurrentHeroTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.NextAbilityComboStat", b =>
                {
                    b.Property<int>("AbilityId");

                    b.Property<int>("ComboId");

                    b.Property<string>("AbilityIdentity");

                    b.Property<string>("AbilityName");

                    b.Property<long>("Assists");

                    b.Property<string>("ComboIdentity");

                    b.Property<string>("ComboName");

                    b.Property<long>("Deaths");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<bool>("SameSource");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("AbilityId", "ComboId");

                    b.ToTable("NextAbilityComboTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.NextAbilityStat", b =>
                {
                    b.Property<int>("AbilityId");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("Identity");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<string>("Name");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("AbilityId");

                    b.ToTable("NextAbilityTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.NextHeroComboStats", b =>
                {
                    b.Property<int>("HeroId");

                    b.Property<int>("AbilityId");

                    b.Property<string>("AbilityIdentity");

                    b.Property<string>("AbilityName");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("HeroIdentity");

                    b.Property<string>("HeroName");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<bool>("SameSource");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("HeroId", "AbilityId");

                    b.ToTable("NextHeroComboTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.NextHeroStat", b =>
                {
                    b.Property<int>("HeroId");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("Identity");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<string>("Name");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("HeroId");

                    b.ToTable("NextHeroTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.PerviousAbilityComboStat", b =>
                {
                    b.Property<int>("AbilityId");

                    b.Property<int>("ComboId");

                    b.Property<string>("AbilityIdentity");

                    b.Property<string>("AbilityName");

                    b.Property<long>("Assists");

                    b.Property<string>("ComboIdentity");

                    b.Property<string>("ComboName");

                    b.Property<long>("Deaths");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<bool>("SameSource");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("AbilityId", "ComboId");

                    b.ToTable("PerviousAbilityComboTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.PerviousAbilityStat", b =>
                {
                    b.Property<int>("AbilityId");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("Identity");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<string>("Name");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("AbilityId");

                    b.ToTable("PerviousAbilityTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.PerviousHeroComboStats", b =>
                {
                    b.Property<int>("HeroId");

                    b.Property<int>("AbilityId");

                    b.Property<string>("AbilityIdentity");

                    b.Property<string>("AbilityName");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("HeroIdentity");

                    b.Property<string>("HeroName");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<bool>("SameSource");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("HeroId", "AbilityId");

                    b.ToTable("PerviousHeroComboTrends");
                });

            modelBuilder.Entity("HGV.AD.Web.Models.Trends.PerviousHeroStat", b =>
                {
                    b.Property<int>("HeroId");

                    b.Property<long>("Assists");

                    b.Property<long>("Deaths");

                    b.Property<string>("Identity");

                    b.Property<long>("Kills");

                    b.Property<long>("Loses");

                    b.Property<string>("Name");

                    b.Property<long>("Total");

                    b.Property<long>("Wins");

                    b.HasKey("HeroId");

                    b.ToTable("PerviousHeroTrends");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HGV.AD.Web.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HGV.AD.Web.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HGV.AD.Web.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
