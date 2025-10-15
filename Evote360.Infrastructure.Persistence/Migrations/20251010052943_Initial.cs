using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evote360.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectivePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectivePositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliticalParty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalParty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PoliticalPartyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_PoliticalParty_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoliticalAlliances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestingPartyId = table.Column<int>(type: "int", nullable: false),
                    TargetPartyId = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Pending"),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalAlliances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliticalAlliances_PoliticalParty_RequestingPartyId",
                        column: x => x.RequestingPartyId,
                        principalTable: "PoliticalParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoliticalAlliances_PoliticalParty_TargetPartyId",
                        column: x => x.TargetPartyId,
                        principalTable: "PoliticalParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyLeaderAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PoliticalPartyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyLeaderAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyLeaderAssignments_PoliticalParty_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyLeaderAssignments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CandidatePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    ElectivePositionId = table.Column<int>(type: "int", nullable: false),
                    PoliticalPartyId = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidatePositions_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CandidatePositions_ElectivePositions_ElectivePositionId",
                        column: x => x.ElectivePositionId,
                        principalTable: "ElectivePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CandidatePositions_PoliticalParty_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    ElectionId = table.Column<int>(type: "int", nullable: false),
                    ElectivePositionId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: true),
                    VoteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_ElectivePositions_ElectivePositionId",
                        column: x => x.ElectivePositionId,
                        principalTable: "ElectivePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePositions_CandidateId",
                table: "CandidatePositions",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePositions_ElectivePositionId",
                table: "CandidatePositions",
                column: "ElectivePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePositions_PoliticalPartyId",
                table: "CandidatePositions",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PoliticalPartyId",
                table: "Candidates",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyLeaderAssignments_PoliticalPartyId",
                table: "PartyLeaderAssignments",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyLeaderAssignments_UserId",
                table: "PartyLeaderAssignments",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAlliances_RequestingPartyId_TargetPartyId",
                table: "PoliticalAlliances",
                columns: new[] { "RequestingPartyId", "TargetPartyId" });

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAlliances_TargetPartyId",
                table: "PoliticalAlliances",
                column: "TargetPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalParty_Acronym",
                table: "PoliticalParty",
                column: "Acronym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CitizenId_ElectionId_ElectivePositionId",
                table: "Votes",
                columns: new[] { "CitizenId", "ElectionId", "ElectivePositionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ElectionId",
                table: "Votes",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ElectivePositionId",
                table: "Votes",
                column: "ElectivePositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidatePositions");

            migrationBuilder.DropTable(
                name: "PartyLeaderAssignments");

            migrationBuilder.DropTable(
                name: "PoliticalAlliances");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Citizens");

            migrationBuilder.DropTable(
                name: "Elections");

            migrationBuilder.DropTable(
                name: "ElectivePositions");

            migrationBuilder.DropTable(
                name: "PoliticalParty");
        }
    }
}
