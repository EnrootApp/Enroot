using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enroot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_TenantId",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Id",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TenantId_UserId",
                table: "Accounts",
                columns: new[] { "TenantId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_TenantId_UserId",
                table: "Accounts");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TenantId",
                table: "Accounts",
                column: "TenantId");
        }
    }
}
