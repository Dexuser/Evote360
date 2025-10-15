using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evote360.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ElectionCandidateTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CandidatePositions");

            migrationBuilder.CreateTable(
                name: "ElectionCandidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    PoliticalPartyId = table.Column<int>(type: "int", nullable: false),
                    ElectivePositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionCandidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionCandidates_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectionCandidates_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectionCandidates_ElectivePositions_ElectivePositionId",
                        column: x => x.ElectivePositionId,
                        principalTable: "ElectivePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectionCandidates_PoliticalParty_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCandidates_CandidateId",
                table: "ElectionCandidates",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCandidates_ElectionId",
                table: "ElectionCandidates",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCandidates_ElectivePositionId",
                table: "ElectionCandidates",
                column: "ElectivePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCandidates_PoliticalPartyId",
                table: "ElectionCandidates",
                column: "PoliticalPartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectionCandidates");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CandidatePositions",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }
    }
}
