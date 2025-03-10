using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTmpScan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempScanItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScanTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanNumber = table.Column<int>(type: "int", nullable: false),
                    ActualNumber = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineProd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempScanItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempScanItems");
        }
    }
}
