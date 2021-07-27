using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class bag_price_one_to_one_airlineid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BagPriceMasters_AirlineId",
                table: "BagPriceMasters");

            migrationBuilder.CreateIndex(
                name: "IX_BagPriceMasters_AirlineId",
                table: "BagPriceMasters",
                column: "AirlineId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BagPriceMasters_AirlineId",
                table: "BagPriceMasters");

            migrationBuilder.CreateIndex(
                name: "IX_BagPriceMasters_AirlineId",
                table: "BagPriceMasters",
                column: "AirlineId");
        }
    }
}
