using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HGV.AD.Web.Data.Migrations
{
    public partial class UpdateHeroAttributeRankToAddHeroIdRankAndValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeroId",
                table: "HeroAttributeRanks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "HeroAttributeRanks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "HeroAttributeRanks",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeroId",
                table: "HeroAttributeRanks");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "HeroAttributeRanks");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "HeroAttributeRanks");
        }
    }
}
