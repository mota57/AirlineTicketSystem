using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class renameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirlineAirport_Airlines_AirlinesId",
                table: "AirlineAirport");

            migrationBuilder.DropForeignKey(
                name: "FK_AirlineAirport_Airports_AirportsId",
                table: "AirlineAirport");

            migrationBuilder.RenameColumn(
                name: "AirportsId",
                table: "AirlineAirport",
                newName: "AirportId");

            migrationBuilder.RenameColumn(
                name: "AirlinesId",
                table: "AirlineAirport",
                newName: "AirlineId");

            migrationBuilder.RenameIndex(
                name: "IX_AirlineAirport_AirportsId",
                table: "AirlineAirport",
                newName: "IX_AirlineAirport_AirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineAirport_Airlines_AirlineId",
                table: "AirlineAirport",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineAirport_Airports_AirportId",
                table: "AirlineAirport",
                column: "AirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirlineAirport_Airlines_AirlineId",
                table: "AirlineAirport");

            migrationBuilder.DropForeignKey(
                name: "FK_AirlineAirport_Airports_AirportId",
                table: "AirlineAirport");

            migrationBuilder.RenameColumn(
                name: "AirportId",
                table: "AirlineAirport",
                newName: "AirportsId");

            migrationBuilder.RenameColumn(
                name: "AirlineId",
                table: "AirlineAirport",
                newName: "AirlinesId");

            migrationBuilder.RenameIndex(
                name: "IX_AirlineAirport_AirportId",
                table: "AirlineAirport",
                newName: "IX_AirlineAirport_AirportsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineAirport_Airlines_AirlinesId",
                table: "AirlineAirport",
                column: "AirlinesId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineAirport_Airports_AirportsId",
                table: "AirlineAirport",
                column: "AirportsId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
