using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class adding_airlineid_for_gate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirlineId",
                table: "Gates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gates_AirlineId",
                table: "Gates",
                column: "AirlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gates_Airlines_AirlineId",
                table: "Gates",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gates_Airlines_AirlineId",
                table: "Gates");

            migrationBuilder.DropIndex(
                name: "IX_Gates_AirlineId",
                table: "Gates");

            migrationBuilder.DropColumn(
                name: "AirlineId",
                table: "Gates");
        }
    }
}
