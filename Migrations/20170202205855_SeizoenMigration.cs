using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrisnaldoApp.Migrations
{
    public partial class SeizoenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Seizoen",
                columns: table => new
                {
                    SeizoenID = table.Column<int>(nullable: false),
                    EindDatum = table.Column<DateTime>(nullable: false),
                    SeizoenNaam = table.Column<string>(nullable: true),
                    StartDatum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seizoen", x => x.SeizoenID);
                });

            migrationBuilder.AddColumn<int>(
                name: "SeizoenID",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Match_SeizoenID",
                table: "Match",
                column: "SeizoenID");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Seizoen_SeizoenID",
                table: "Match",
                column: "SeizoenID",
                principalTable: "Seizoen",
                principalColumn: "SeizoenID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Seizoen_SeizoenID",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_SeizoenID",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "SeizoenID",
                table: "Match");

            migrationBuilder.DropTable(
                name: "Seizoen");
        }
    }
}
