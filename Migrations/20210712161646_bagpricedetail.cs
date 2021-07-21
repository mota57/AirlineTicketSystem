using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class bagpricedetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BagPriceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoundStart = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PoundEnd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BagPriceMasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BagPriceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BagPriceDetails_BagPriceMasters_BagPriceMasterId",
                        column: x => x.BagPriceMasterId,
                        principalTable: "BagPriceMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BagPriceDetails_BagPriceMasterId",
                table: "BagPriceDetails",
                column: "BagPriceMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BagPriceDetails");
        }
    }
}
