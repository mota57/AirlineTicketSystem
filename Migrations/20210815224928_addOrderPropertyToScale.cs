using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class addOrderPropertyToScale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirlineGate_Airlines_AirlineId",
                table: "AirlineGate");

            migrationBuilder.DropForeignKey(
                name: "FK_AirlineGate_Gates_GateId",
                table: "AirlineGate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AirlineGate",
                table: "AirlineGate");

            migrationBuilder.RenameTable(
                name: "AirlineGate",
                newName: "AirlineGates");

            migrationBuilder.RenameIndex(
                name: "IX_AirlineGate_GateId",
                table: "AirlineGates",
                newName: "IX_AirlineGates_GateId");

            migrationBuilder.RenameIndex(
                name: "IX_AirlineGate_AirlineId",
                table: "AirlineGates",
                newName: "IX_AirlineGates_AirlineId");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "FlightScales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AirportId",
                table: "AirlineGates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AirlineGates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AirlineGates",
                table: "AirlineGates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineGates_AirportId",
                table: "AirlineGates",
                column: "AirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineGates_Airlines_AirlineId",
                table: "AirlineGates",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineGates_Airports_AirportId",
                table: "AirlineGates",
                column: "AirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineGates_Gates_GateId",
                table: "AirlineGates",
                column: "GateId",
                principalTable: "Gates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirlineGates_Airlines_AirlineId",
                table: "AirlineGates");

            migrationBuilder.DropForeignKey(
                name: "FK_AirlineGates_Airports_AirportId",
                table: "AirlineGates");

            migrationBuilder.DropForeignKey(
                name: "FK_AirlineGates_Gates_GateId",
                table: "AirlineGates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AirlineGates",
                table: "AirlineGates");

            migrationBuilder.DropIndex(
                name: "IX_AirlineGates_AirportId",
                table: "AirlineGates");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "FlightScales");

            migrationBuilder.DropColumn(
                name: "AirportId",
                table: "AirlineGates");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AirlineGates");

            migrationBuilder.RenameTable(
                name: "AirlineGates",
                newName: "AirlineGate");

            migrationBuilder.RenameIndex(
                name: "IX_AirlineGates_GateId",
                table: "AirlineGate",
                newName: "IX_AirlineGate_GateId");

            migrationBuilder.RenameIndex(
                name: "IX_AirlineGates_AirlineId",
                table: "AirlineGate",
                newName: "IX_AirlineGate_AirlineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AirlineGate",
                table: "AirlineGate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineGate_Airlines_AirlineId",
                table: "AirlineGate",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineGate_Gates_GateId",
                table: "AirlineGate",
                column: "GateId",
                principalTable: "Gates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
