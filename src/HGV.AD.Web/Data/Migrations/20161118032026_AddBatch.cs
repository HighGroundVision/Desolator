using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HGV.AD.Web.Data.Migrations
{
    public partial class AddBatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batchs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateProcessed = table.Column<DateTime>(nullable: false),
                    NumberOfAD = table.Column<int>(nullable: false),
                    NumberOfMatches = table.Column<int>(nullable: false),
                    SquenceEnd = table.Column<long>(nullable: false),
                    SquenceStart = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batchs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batchs");
        }
    }
}
