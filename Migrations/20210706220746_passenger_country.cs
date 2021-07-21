using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class passenger_country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Passengers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_CountryId",
                table: "Passengers",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Countries_CountryId",
                table: "Passengers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Countries_CountryId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_CountryId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Passengers");
        }
    }
}
