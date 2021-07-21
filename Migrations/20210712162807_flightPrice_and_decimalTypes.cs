using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class flightPrice_and_decimalTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightPrices_Flights_TicketId",
                table: "FlightPrices");

            migrationBuilder.DropIndex(
                name: "IX_FlightPrices_TicketId",
                table: "FlightPrices");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "FlightPrices");

            migrationBuilder.RenameColumn(
                name: "TicketPriceId",
                table: "FlightPrices",
                newName: "FlightScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPrices_FlightScaleId",
                table: "FlightPrices",
                column: "FlightScaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPrices_Flights_FlightScaleId",
                table: "FlightPrices",
                column: "FlightScaleId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightPrices_Flights_FlightScaleId",
                table: "FlightPrices");

            migrationBuilder.DropIndex(
                name: "IX_FlightPrices_FlightScaleId",
                table: "FlightPrices");

            migrationBuilder.RenameColumn(
                name: "FlightScaleId",
                table: "FlightPrices",
                newName: "TicketPriceId");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "FlightPrices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightPrices_TicketId",
                table: "FlightPrices",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPrices_Flights_TicketId",
                table: "FlightPrices",
                column: "TicketId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
