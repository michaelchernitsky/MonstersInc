using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonstersInc.Data.Migrations.IdentityServer.MonstersIncDb
{
    public partial class InitialMonstersIncDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    AvailableAmountOfEnergy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkDayPerformance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntimidatorId = table.Column<Guid>(nullable: false),
                    WorkDayDate = table.Column<DateTime>(nullable: false),
                    ExpectedEnergyAmount = table.Column<int>(nullable: false),
                    ActualEnergyAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDayPerformance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepletedDoors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkDayPerformanceId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DoorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepletedDoors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepletedDoors_Doors_DoorId",
                        column: x => x.DoorId,
                        principalTable: "Doors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepletedDoors_WorkDayPerformance_WorkDayPerformanceId",
                        column: x => x.WorkDayPerformanceId,
                        principalTable: "WorkDayPerformance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepletedDoors_DoorId",
                table: "DepletedDoors",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_DepletedDoors_WorkDayPerformanceId",
                table: "DepletedDoors",
                column: "WorkDayPerformanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepletedDoors");

            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropTable(
                name: "WorkDayPerformance");
        }
    }
}
