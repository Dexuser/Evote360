using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evote360.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CitizenFieldsIdentityNumberAndEmailAreUNQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                table: "Citizens",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_Email",
                table: "Citizens",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_IdentityNumber",
                table: "Citizens",
                column: "IdentityNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Citizens_Email",
                table: "Citizens");

            migrationBuilder.DropIndex(
                name: "IX_Citizens_IdentityNumber",
                table: "Citizens");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                table: "Citizens",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);
        }
    }
}
