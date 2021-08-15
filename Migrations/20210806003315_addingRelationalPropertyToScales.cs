using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class addingRelationalPropertyToScales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightBagPayment_FlightScale_FlightScaleId",
                table: "FlightBagPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightPrices_FlightScale_FlightScaleId",
                table: "FlightPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScale_Airlines_AirlineId",
                table: "FlightScale");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScale_Airplanes_AirplaneId",
                table: "FlightScale");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScale_Airports_AirportArrivalId",
                table: "FlightScale");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScale_Airports_AirportDepartureId",
                table: "FlightScale");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScale_Flights_FlightId",
                table: "FlightScale");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScale_Gates_GateId",
                table: "FlightScale");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScale_Terminals_TerminalId",
                table: "FlightScale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightScale",
                table: "FlightScale");

            migrationBuilder.RenameTable(
                name: "FlightScale",
                newName: "FlightScales");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScale_TerminalId",
                table: "FlightScales",
                newName: "IX_FlightScales_TerminalId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScale_GateId",
                table: "FlightScales",
                newName: "IX_FlightScales_GateId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScale_FlightId",
                table: "FlightScales",
                newName: "IX_FlightScales_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScale_AirportDepartureId",
                table: "FlightScales",
                newName: "IX_FlightScales_AirportDepartureId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScale_AirportArrivalId",
                table: "FlightScales",
                newName: "IX_FlightScales_AirportArrivalId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScale_AirplaneId",
                table: "FlightScales",
                newName: "IX_FlightScales_AirplaneId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScale_AirlineId",
                table: "FlightScales",
                newName: "IX_FlightScales_AirlineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightScales",
                table: "FlightScales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightBagPayment_FlightScales_FlightScaleId",
                table: "FlightBagPayment",
                column: "FlightScaleId",
                principalTable: "FlightScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPrices_FlightScales_FlightScaleId",
                table: "FlightPrices",
                column: "FlightScaleId",
                principalTable: "FlightScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScales_Airlines_AirlineId",
                table: "FlightScales",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScales_Airplanes_AirplaneId",
                table: "FlightScales",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScales_Airports_AirportArrivalId",
                table: "FlightScales",
                column: "AirportArrivalId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScales_Airports_AirportDepartureId",
                table: "FlightScales",
                column: "AirportDepartureId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScales_Flights_FlightId",
                table: "FlightScales",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScales_Gates_GateId",
                table: "FlightScales",
                column: "GateId",
                principalTable: "Gates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScales_Terminals_TerminalId",
                table: "FlightScales",
                column: "TerminalId",
                principalTable: "Terminals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightBagPayment_FlightScales_FlightScaleId",
                table: "FlightBagPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightPrices_FlightScales_FlightScaleId",
                table: "FlightPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScales_Airlines_AirlineId",
                table: "FlightScales");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScales_Airplanes_AirplaneId",
                table: "FlightScales");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScales_Airports_AirportArrivalId",
                table: "FlightScales");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScales_Airports_AirportDepartureId",
                table: "FlightScales");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScales_Flights_FlightId",
                table: "FlightScales");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScales_Gates_GateId",
                table: "FlightScales");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightScales_Terminals_TerminalId",
                table: "FlightScales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightScales",
                table: "FlightScales");

            migrationBuilder.RenameTable(
                name: "FlightScales",
                newName: "FlightScale");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScales_TerminalId",
                table: "FlightScale",
                newName: "IX_FlightScale_TerminalId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScales_GateId",
                table: "FlightScale",
                newName: "IX_FlightScale_GateId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScales_FlightId",
                table: "FlightScale",
                newName: "IX_FlightScale_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScales_AirportDepartureId",
                table: "FlightScale",
                newName: "IX_FlightScale_AirportDepartureId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScales_AirportArrivalId",
                table: "FlightScale",
                newName: "IX_FlightScale_AirportArrivalId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScales_AirplaneId",
                table: "FlightScale",
                newName: "IX_FlightScale_AirplaneId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightScales_AirlineId",
                table: "FlightScale",
                newName: "IX_FlightScale_AirlineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightScale",
                table: "FlightScale",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightBagPayment_FlightScale_FlightScaleId",
                table: "FlightBagPayment",
                column: "FlightScaleId",
                principalTable: "FlightScale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPrices_FlightScale_FlightScaleId",
                table: "FlightPrices",
                column: "FlightScaleId",
                principalTable: "FlightScale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScale_Airlines_AirlineId",
                table: "FlightScale",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScale_Airplanes_AirplaneId",
                table: "FlightScale",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScale_Airports_AirportArrivalId",
                table: "FlightScale",
                column: "AirportArrivalId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScale_Airports_AirportDepartureId",
                table: "FlightScale",
                column: "AirportDepartureId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScale_Flights_FlightId",
                table: "FlightScale",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScale_Gates_GateId",
                table: "FlightScale",
                column: "GateId",
                principalTable: "Gates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScale_Terminals_TerminalId",
                table: "FlightScale",
                column: "TerminalId",
                principalTable: "Terminals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
