using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FooCargo.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manifests",
                columns: table => new
                {
                    FltNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifests", x => new { x.FltNo, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    MailType = table.Column<int>(type: "int", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => new { x.MailType, x.Origin, x.Destination });
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    AWBNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailType = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    NumberOfItems = table.Column<int>(type: "int", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ManifestFlightNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ManifestDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.AWBNumber);
                    table.ForeignKey(
                        name: "FK_Shipments_Manifests_ManifestFlightNumber_ManifestDate",
                        columns: x => new { x.ManifestFlightNumber, x.ManifestDate },
                        principalTable: "Manifests",
                        principalColumns: new[] { "FltNo", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ManifestFlightNumber_ManifestDate",
                table: "Shipments",
                columns: new[] { "ManifestFlightNumber", "ManifestDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Manifests");
        }
    }
}
