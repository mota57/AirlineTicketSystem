using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class setAirlineIdNullAbleForBagPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BagPriceMasters_AirlineId",
                table: "BagPriceMasters");

            migrationBuilder.AlterColumn<int>(
                name: "AirlineId",
                table: "BagPriceMasters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AirlineAssociated",
                table: "BagPriceMasters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BagPriceMasters_AirlineId",
                table: "BagPriceMasters",
                column: "AirlineId",
                unique: true,
                filter: "[AirlineId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BagPriceMasters_AirlineId",
                table: "BagPriceMasters");

            migrationBuilder.DropColumn(
                name: "AirlineAssociated",
                table: "BagPriceMasters");

            migrationBuilder.AlterColumn<int>(
                name: "AirlineId",
                table: "BagPriceMasters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BagPriceMasters_AirlineId",
                table: "BagPriceMasters",
                column: "AirlineId",
                unique: true);
        }
    }
}
