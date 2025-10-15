using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evote360.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ElectionPartyAndElectionPositionTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectionParty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionId = table.Column<int>(type: "int", nullable: false),
                    PoliticalPartyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionParty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionParty_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectionParty_PoliticalParty_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectionPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionId = table.Column<int>(type: "int", nullable: false),
                    ElectivePositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionPosition_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectionPosition_ElectivePositions_ElectivePositionId",
                        column: x => x.ElectivePositionId,
                        principalTable: "ElectivePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectionParty_ElectionId_PoliticalPartyId",
                table: "ElectionParty",
                columns: new[] { "ElectionId", "PoliticalPartyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectionParty_PoliticalPartyId",
                table: "ElectionParty",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionPosition_ElectionId_ElectivePositionId",
                table: "ElectionPosition",
                columns: new[] { "ElectionId", "ElectivePositionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectionPosition_ElectivePositionId",
                table: "ElectionPosition",
                column: "ElectivePositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectionParty");

            migrationBuilder.DropTable(
                name: "ElectionPosition");
        }
    }
}
