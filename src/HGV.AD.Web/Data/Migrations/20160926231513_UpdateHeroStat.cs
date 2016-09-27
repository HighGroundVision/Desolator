using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HGV.AD.Web.Data.Migrations
{
    public partial class UpdateHeroStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alignment",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "AttackPoint",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "AttackSwing",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "BaseAttackTime",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "CastPoint",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "CastSwing",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "LookupId",
                table: "Heroes");

            migrationBuilder.AddColumn<double>(
                name: "AvgDmg",
                table: "Heroes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Primary",
                table: "Heroes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgDmg",
                table: "Heroes");

            migrationBuilder.AddColumn<int>(
                name: "Alignment",
                table: "Heroes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AttackPoint",
                table: "Heroes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AttackSwing",
                table: "Heroes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BaseAttackTime",
                table: "Heroes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CastPoint",
                table: "Heroes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CastSwing",
                table: "Heroes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LookupId",
                table: "Heroes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Primary",
                table: "Heroes",
                nullable: false);
        }
    }
}
