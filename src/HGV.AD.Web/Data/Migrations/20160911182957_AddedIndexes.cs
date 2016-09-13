using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HGV.AD.Web.Data.Migrations
{
    public partial class AddedIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PerviousHeroTrends_HeroId",
                table: "PerviousHeroTrends",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_PerviousHeroComboTrends_AbilityId",
                table: "PerviousHeroComboTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PerviousHeroComboTrends_HeroId",
                table: "PerviousHeroComboTrends",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_PerviousAbilityTrends_AbilityId",
                table: "PerviousAbilityTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PerviousAbilityComboTrends_AbilityId",
                table: "PerviousAbilityComboTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PerviousAbilityComboTrends_ComboId",
                table: "PerviousAbilityComboTrends",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_NextHeroTrends_HeroId",
                table: "NextHeroTrends",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_NextHeroComboTrends_AbilityId",
                table: "NextHeroComboTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NextHeroComboTrends_HeroId",
                table: "NextHeroComboTrends",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_NextAbilityTrends_AbilityId",
                table: "NextAbilityTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NextAbilityComboTrends_AbilityId",
                table: "NextAbilityComboTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NextAbilityComboTrends_ComboId",
                table: "NextAbilityComboTrends",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentHeroTrends_HeroId",
                table: "CurrentHeroTrends",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentHeroComboTrends_AbilityId",
                table: "CurrentHeroComboTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentHeroComboTrends_HeroId",
                table: "CurrentHeroComboTrends",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAbilityTrends_AbilityId",
                table: "CurrentAbilityTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAbilityComboTrends_AbilityId",
                table: "CurrentAbilityComboTrends",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAbilityComboTrends_ComboId",
                table: "CurrentAbilityComboTrends",
                column: "ComboId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PerviousHeroTrends_HeroId",
                table: "PerviousHeroTrends");

            migrationBuilder.DropIndex(
                name: "IX_PerviousHeroComboTrends_AbilityId",
                table: "PerviousHeroComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_PerviousHeroComboTrends_HeroId",
                table: "PerviousHeroComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_PerviousAbilityTrends_AbilityId",
                table: "PerviousAbilityTrends");

            migrationBuilder.DropIndex(
                name: "IX_PerviousAbilityComboTrends_AbilityId",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_PerviousAbilityComboTrends_ComboId",
                table: "PerviousAbilityComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_NextHeroTrends_HeroId",
                table: "NextHeroTrends");

            migrationBuilder.DropIndex(
                name: "IX_NextHeroComboTrends_AbilityId",
                table: "NextHeroComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_NextHeroComboTrends_HeroId",
                table: "NextHeroComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_NextAbilityTrends_AbilityId",
                table: "NextAbilityTrends");

            migrationBuilder.DropIndex(
                name: "IX_NextAbilityComboTrends_AbilityId",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_NextAbilityComboTrends_ComboId",
                table: "NextAbilityComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_CurrentHeroTrends_HeroId",
                table: "CurrentHeroTrends");

            migrationBuilder.DropIndex(
                name: "IX_CurrentHeroComboTrends_AbilityId",
                table: "CurrentHeroComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_CurrentHeroComboTrends_HeroId",
                table: "CurrentHeroComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAbilityTrends_AbilityId",
                table: "CurrentAbilityTrends");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAbilityComboTrends_AbilityId",
                table: "CurrentAbilityComboTrends");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAbilityComboTrends_ComboId",
                table: "CurrentAbilityComboTrends");
        }
    }
}
