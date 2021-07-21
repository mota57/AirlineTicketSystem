using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class renameTableRelateToFlightBagPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BagFlights_BagPriceDetails_BagPriceDetailId",
                table: "BagFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_BagFlights_FlightScale_FlightScaleId",
                table: "BagFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_BagFlights_Passengers_PassengerId",
                table: "BagFlights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BagFlights",
                table: "BagFlights");

            migrationBuilder.RenameTable(
                name: "BagFlights",
                newName: "FlightBagPayment");

            migrationBuilder.RenameIndex(
                name: "IX_BagFlights_PassengerId",
                table: "FlightBagPayment",
                newName: "IX_FlightBagPayment_PassengerId");

            migrationBuilder.RenameIndex(
                name: "IX_BagFlights_FlightScaleId",
                table: "FlightBagPayment",
                newName: "IX_FlightBagPayment_FlightScaleId");

            migrationBuilder.RenameIndex(
                name: "IX_BagFlights_BagPriceDetailId",
                table: "FlightBagPayment",
                newName: "IX_FlightBagPayment_BagPriceDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightBagPayment",
                table: "FlightBagPayment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightBagPayment_BagPriceDetails_BagPriceDetailId",
                table: "FlightBagPayment",
                column: "BagPriceDetailId",
                principalTable: "BagPriceDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightBagPayment_FlightScale_FlightScaleId",
                table: "FlightBagPayment",
                column: "FlightScaleId",
                principalTable: "FlightScale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightBagPayment_Passengers_PassengerId",
                table: "FlightBagPayment",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightBagPayment_BagPriceDetails_BagPriceDetailId",
                table: "FlightBagPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightBagPayment_FlightScale_FlightScaleId",
                table: "FlightBagPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightBagPayment_Passengers_PassengerId",
                table: "FlightBagPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightBagPayment",
                table: "FlightBagPayment");

            migrationBuilder.RenameTable(
                name: "FlightBagPayment",
                newName: "BagFlights");

            migrationBuilder.RenameIndex(
                name: "IX_FlightBagPayment_PassengerId",
                table: "BagFlights",
                newName: "IX_BagFlights_PassengerId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightBagPayment_FlightScaleId",
                table: "BagFlights",
                newName: "IX_BagFlights_FlightScaleId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightBagPayment_BagPriceDetailId",
                table: "BagFlights",
                newName: "IX_BagFlights_BagPriceDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BagFlights",
                table: "BagFlights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BagFlights_BagPriceDetails_BagPriceDetailId",
                table: "BagFlights",
                column: "BagPriceDetailId",
                principalTable: "BagPriceDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BagFlights_FlightScale_FlightScaleId",
                table: "BagFlights",
                column: "FlightScaleId",
                principalTable: "FlightScale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BagFlights_Passengers_PassengerId",
                table: "BagFlights",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
