using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class addFlightScaleRelationToFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "FlightScale",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FlightScale_FlightId",
                table: "FlightScale",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightScale_Flights_FlightId",
                table: "FlightScale",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightScale_Flights_FlightId",
                table: "FlightScale");

            migrationBuilder.DropIndex(
                name: "IX_FlightScale_FlightId",
                table: "FlightScale");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "FlightScale");
        }
    }
}
