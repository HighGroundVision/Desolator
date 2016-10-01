using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HGV.AD.Web.Data.Migrations
{
    public partial class AddedSourceSameToCombos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SameSource",
                table: "PerviousHeroComboTrends",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SameSource",
                table: "PerviousAbilityComboTrends",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SameSource",
                table: "NextHeroComboTrends",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SameSource",
                table: "NextAbilityComboTrends",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SameSource",
                table: "CurrentHeroComboTrends",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SameSource",
                table: "CurrentAbilityComboTrends",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SameSource",
                table: "PerviousHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "SameSource",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "SameSource",
                table: "NextHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "SameSource",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropColumn(
                name: "SameSource",
                table: "CurrentHeroComboTrends");

            migrationBuilder.DropColumn(
                name: "SameSource",
                table: "CurrentAbilityComboTrends");
        }
    }
}
