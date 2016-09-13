using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HGV.AD.Web.Data.Migrations
{
    public partial class ImportedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AffectsWhom = table.Column<string>(nullable: true),
                    DamageType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HeroId = table.Column<int>(nullable: false),
                    Html = table.Column<string>(nullable: true),
                    Identity = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    Ultimate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AgiGain = table.Column<double>(nullable: false),
                    Alignment = table.Column<int>(nullable: false),
                    Armor = table.Column<double>(nullable: false),
                    AttackPoint = table.Column<double>(nullable: false),
                    AttackSwing = table.Column<double>(nullable: false),
                    BaseAgi = table.Column<double>(nullable: false),
                    BaseAttackTime = table.Column<double>(nullable: false),
                    BaseInt = table.Column<double>(nullable: false),
                    BaseStr = table.Column<double>(nullable: false),
                    CastPoint = table.Column<double>(nullable: false),
                    CastSwing = table.Column<double>(nullable: false),
                    DayVision = table.Column<double>(nullable: false),
                    HP = table.Column<double>(nullable: false),
                    HPRegen = table.Column<double>(nullable: false),
                    Identity = table.Column<string>(nullable: true),
                    IntGain = table.Column<double>(nullable: false),
                    LookupId = table.Column<int>(nullable: false),
                    Mana = table.Column<double>(nullable: false),
                    ManaRegen = table.Column<double>(nullable: false),
                    MaxDmg = table.Column<double>(nullable: false),
                    MinDmg = table.Column<double>(nullable: false),
                    Movespeed = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NightVision = table.Column<double>(nullable: false),
                    Patch = table.Column<string>(nullable: true),
                    Primary = table.Column<int>(nullable: false),
                    ProjectileSpeed = table.Column<double>(nullable: false),
                    Range = table.Column<double>(nullable: false),
                    StrGain = table.Column<double>(nullable: false),
                    Turnrate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAbilityComboTrends",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    ComboAbilityId = table.Column<int>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAbilityComboTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAbilityTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAbilityTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentHeroComboTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    HeroId = table.Column<int>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentHeroComboTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentHeroTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    HeroId = table.Column<int>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentHeroTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NextAbilityComboTrends",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    ComboAbilityId = table.Column<int>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextAbilityComboTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NextAbilityTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextAbilityTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NextHeroComboTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    HeroId = table.Column<int>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextHeroComboTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NextHeroTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    HeroId = table.Column<int>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextHeroTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerviousAbilityComboTrends",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    ComboAbilityId = table.Column<int>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerviousAbilityComboTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerviousAbilityTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerviousAbilityTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerviousHeroComboTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityId = table.Column<int>(nullable: false),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    HeroId = table.Column<int>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerviousHeroComboTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerviousHeroTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assists = table.Column<long>(nullable: false),
                    Deaths = table.Column<long>(nullable: false),
                    HeroId = table.Column<int>(nullable: false),
                    Kills = table.Column<long>(nullable: false),
                    Loses = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false),
                    Wins = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerviousHeroTrends", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "CurrentAbilityComboTrends");

            migrationBuilder.DropTable(
                name: "CurrentAbilityTrends");

            migrationBuilder.DropTable(
                name: "CurrentHeroComboTrends");

            migrationBuilder.DropTable(
                name: "CurrentHeroTrends");

            migrationBuilder.DropTable(
                name: "NextAbilityComboTrends");

            migrationBuilder.DropTable(
                name: "NextAbilityTrends");

            migrationBuilder.DropTable(
                name: "NextHeroComboTrends");

            migrationBuilder.DropTable(
                name: "NextHeroTrends");

            migrationBuilder.DropTable(
                name: "PerviousAbilityComboTrends");

            migrationBuilder.DropTable(
                name: "PerviousAbilityTrends");

            migrationBuilder.DropTable(
                name: "PerviousHeroComboTrends");

            migrationBuilder.DropTable(
                name: "PerviousHeroTrends");
        }
    }
}
