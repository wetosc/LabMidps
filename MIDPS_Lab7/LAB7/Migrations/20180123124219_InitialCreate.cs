using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LAB7.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ring",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Material = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ring", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Wizard",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Color = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wizard", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WizardRing",
                columns: table => new
                {
                    WizardID = table.Column<int>(nullable: false),
                    RingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WizardRing", x => new { x.WizardID, x.RingID });
                    table.ForeignKey(
                        name: "FK_WizardRing_Ring_RingID",
                        column: x => x.RingID,
                        principalTable: "Ring",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WizardRing_Wizard_WizardID",
                        column: x => x.WizardID,
                        principalTable: "Wizard",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WizardRing_RingID",
                table: "WizardRing",
                column: "RingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WizardRing");

            migrationBuilder.DropTable(
                name: "Ring");

            migrationBuilder.DropTable(
                name: "Wizard");
        }
    }
}
