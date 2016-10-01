using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HGV.AD.Web.Data.Migrations
{
    public partial class AddNotesToHeroAbility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Heroes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Abilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Abilities");
        }
    }
}
