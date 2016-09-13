using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HGV.AD.Web.Data.Migrations
{
    public partial class AddIdenityAndNameToTrends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComboAbilityId",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboAbilityId",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboAbilityId",
                table: "CurrentAbilityComboTrends");

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "PerviousHeroTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PerviousHeroTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityIdentity",
                table: "PerviousHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityName",
                table: "PerviousHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroIdentity",
                table: "PerviousHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroName",
                table: "PerviousHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "PerviousAbilityTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PerviousAbilityTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityIdentity",
                table: "PerviousAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityName",
                table: "PerviousAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComboId",
                table: "PerviousAbilityComboTrends",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ComboIdentity",
                table: "PerviousAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComboName",
                table: "PerviousAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "NextHeroTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "NextHeroTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityIdentity",
                table: "NextHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityName",
                table: "NextHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroIdentity",
                table: "NextHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroName",
                table: "NextHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "NextAbilityTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "NextAbilityTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityIdentity",
                table: "NextAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityName",
                table: "NextAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComboId",
                table: "NextAbilityComboTrends",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ComboIdentity",
                table: "NextAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComboName",
                table: "NextAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "CurrentHeroTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CurrentHeroTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityIdentity",
                table: "CurrentHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityName",
                table: "CurrentHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroIdentity",
                table: "CurrentHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroName",
                table: "CurrentHeroComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "CurrentAbilityTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CurrentAbilityTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityIdentity",
                table: "CurrentAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbilityName",
                table: "CurrentAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComboId",
                table: "CurrentAbilityComboTrends",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ComboIdentity",
                table: "CurrentAbilityComboTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComboName",
                table: "CurrentAbilityComboTrends",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identity",
                table: "PerviousHeroTrends");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PerviousHeroTrends");

            migrationBuilder.DropColumn(
                name: "AbilityIdentity",
                table: "PerviousHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "AbilityName",
                table: "PerviousHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "HeroIdentity",
                table: "PerviousHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "HeroName",
                table: "PerviousHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "Identity",
                table: "PerviousAbilityTrends");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PerviousAbilityTrends");

            migrationBuilder.DropColumn(
                name: "AbilityIdentity",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "AbilityName",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboId",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboIdentity",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboName",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "Identity",
                table: "NextHeroTrends");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "NextHeroTrends");

            migrationBuilder.DropColumn(
                name: "AbilityIdentity",
                table: "NextHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "AbilityName",
                table: "NextHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "HeroIdentity",
                table: "NextHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "HeroName",
                table: "NextHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "Identity",
                table: "NextAbilityTrends");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "NextAbilityTrends");

            migrationBuilder.DropColumn(
                name: "AbilityIdentity",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "AbilityName",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboId",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboIdentity",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboName",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "Identity",
                table: "CurrentHeroTrends");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CurrentHeroTrends");

            migrationBuilder.DropColumn(
                name: "AbilityIdentity",
                table: "CurrentHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "AbilityName",
                table: "CurrentHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "HeroIdentity",
                table: "CurrentHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "HeroName",
                table: "CurrentHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "Identity",
                table: "CurrentAbilityTrends");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CurrentAbilityTrends");

            migrationBuilder.DropColumn(
                name: "AbilityIdentity",
                table: "CurrentAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "AbilityName",
                table: "CurrentAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboId",
                table: "CurrentAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboIdentity",
                table: "CurrentAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "ComboName",
                table: "CurrentAbilityComboTrends");

            migrationBuilder.AddColumn<int>(
                name: "ComboAbilityId",
                table: "PerviousAbilityComboTrends",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComboAbilityId",
                table: "NextAbilityComboTrends",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComboAbilityId",
                table: "CurrentAbilityComboTrends",
                nullable: false,
                defaultValue: 0);
        }
    }
}
