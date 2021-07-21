using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class BagFlights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightPrices_Flights_FlightScaleId",
                table: "FlightPrices");

            migrationBuilder.CreateTable(
                name: "FlightScale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirportDepartureId = table.Column<int>(type: "int", nullable: false),
                    AirportArrivalId = table.Column<int>(type: "int", nullable: false),
                    DepartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AirplaneId = table.Column<int>(type: "int", nullable: false),
                    AirlineId = table.Column<int>(type: "int", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TerminalId = table.Column<int>(type: "int", nullable: false),
                    GateId = table.Column<int>(type: "int", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightScale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightScale_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightScale_Airplanes_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Airplanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightScale_Airports_AirportArrivalId",
                        column: x => x.AirportArrivalId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightScale_Airports_AirportDepartureId",
                        column: x => x.AirportDepartureId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightScale_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightScale_Terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BagFlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightScaleId = table.Column<int>(type: "int", nullable: false),
                    Pounds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BagPriceDetailId = table.Column<int>(type: "int", nullable: true),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BagFlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BagFlights_BagPriceDetails_BagPriceDetailId",
                        column: x => x.BagPriceDetailId,
                        principalTable: "BagPriceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BagFlights_FlightScale_FlightScaleId",
                        column: x => x.FlightScaleId,
                        principalTable: "FlightScale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BagFlights_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BagFlights_BagPriceDetailId",
                table: "BagFlights",
                column: "BagPriceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BagFlights_FlightScaleId",
                table: "BagFlights",
                column: "FlightScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_BagFlights_PassengerId",
                table: "BagFlights",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightScale_AirlineId",
                table: "FlightScale",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightScale_AirplaneId",
                table: "FlightScale",
                column: "AirplaneId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightScale_AirportArrivalId",
                table: "FlightScale",
                column: "AirportArrivalId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightScale_AirportDepartureId",
                table: "FlightScale",
                column: "AirportDepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightScale_GateId",
                table: "FlightScale",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightScale_TerminalId",
                table: "FlightScale",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPrices_FlightScale_FlightScaleId",
                table: "FlightPrices",
                column: "FlightScaleId",
                principalTable: "FlightScale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightPrices_FlightScale_FlightScaleId",
                table: "FlightPrices");

            migrationBuilder.DropTable(
                name: "BagFlights");

            migrationBuilder.DropTable(
                name: "FlightScale");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPrices_Flights_FlightScaleId",
                table: "FlightPrices",
                column: "FlightScaleId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
