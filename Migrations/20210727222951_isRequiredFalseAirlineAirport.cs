using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class isRequiredFalseAirlineAirport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AirlineAirport",
                table: "AirlineAirport");

            migrationBuilder.AlterColumn<int>(
                name: "AirportId",
                table: "AirlineAirport",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AirlineId",
                table: "AirlineAirport",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AirlineAirport",
                table: "AirlineAirport",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineAirport_AirlineId",
                table: "AirlineAirport",
                column: "AirlineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AirlineAirport",
                table: "AirlineAirport");

            migrationBuilder.DropIndex(
                name: "IX_AirlineAirport_AirlineId",
                table: "AirlineAirport");

            migrationBuilder.AlterColumn<int>(
                name: "AirportId",
                table: "AirlineAirport",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AirlineId",
                table: "AirlineAirport",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AirlineAirport",
                table: "AirlineAirport",
                columns: new[] { "AirlineId", "AirportId" });
        }
    }
}
