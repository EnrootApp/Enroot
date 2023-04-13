using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enroot.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addpermissiontenantsettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedOn", "IsDeleted" },
                values: new object[] { 5, new DateTime(2023, 4, 12, 18, 40, 25, 265, DateTimeKind.Utc).AddTicks(9740), false });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                values: new object[] { 1, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantAccountIds_Tenants_TenantId",
                table: "TenantAccountIds");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleId", "PermissionId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Roles_TempId",
                table: "Roles",
                column: "TempId");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 12, 12, 18, 18, 400, DateTimeKind.Utc).AddTicks(1500));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 12, 12, 18, 18, 400, DateTimeKind.Utc).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 12, 12, 18, 18, 400, DateTimeKind.Utc).AddTicks(1540));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 12, 12, 18, 18, 400, DateTimeKind.Utc).AddTicks(1550));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "TempId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 12, 12, 18, 18, 400, DateTimeKind.Utc).AddTicks(3270), false, 0 },
                    { 2, new DateTime(2023, 4, 12, 12, 18, 18, 400, DateTimeKind.Utc).AddTicks(3300), false, 0 },
                    { 3, new DateTime(2023, 4, 12, 12, 18, 18, 400, DateTimeKind.Utc).AddTicks(3330), false, 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleId",
                table: "Accounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantAccountIds_Tenants_TenantId",
                table: "TenantAccountIds",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
