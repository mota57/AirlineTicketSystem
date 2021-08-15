using Microsoft.EntityFrameworkCore.Migrations;

namespace AireLineTicketSystem.Migrations
{
    public partial class setGateToHaveManyAirlinesAndViceVersa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "AirlineGate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineId = table.Column<int>(type: "int", nullable: false),
                    GateId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineGate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirlineGate_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineGate_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirlineGate_AirlineId",
                table: "AirlineGate",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineGate_GateId",
                table: "AirlineGate",
                column: "GateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirlineGate");

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
    }
}
