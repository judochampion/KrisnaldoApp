using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrisnaldoApp.Migrations
{
    public partial class AlbumsAndFotoMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    DisplayNaam = table.Column<string>(nullable: true),
                    Intro = table.Column<string>(nullable: true),
                    RuweNaam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.AlbumID);
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    FotoID = table.Column<int>(nullable: false),
                    AlbumID = table.Column<int>(nullable: false),
                    RuweBestandsNaam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.FotoID);
                    table.ForeignKey(
                        name: "FK_Foto_Album_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Album",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foto_AlbumID",
                table: "Foto",
                column: "AlbumID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "Album");
        }
    }
}
