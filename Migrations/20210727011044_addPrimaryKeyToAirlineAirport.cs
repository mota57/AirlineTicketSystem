using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class addPrimaryKeyToAirlineAirport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AirlineAirport",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AirlineAirport");
        }
    }
}
