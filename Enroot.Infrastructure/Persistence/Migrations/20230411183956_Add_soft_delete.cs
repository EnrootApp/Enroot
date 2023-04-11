using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enroot.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addsoftdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tenants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tasqs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "IsDeleted" },
                values: new object[] { new DateTime(2023, 4, 11, 18, 39, 55, 775, DateTimeKind.Utc).AddTicks(3540), false });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "IsDeleted" },
                values: new object[] { new DateTime(2023, 4, 11, 18, 39, 55, 775, DateTimeKind.Utc).AddTicks(3580), false });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "IsDeleted" },
                values: new object[] { new DateTime(2023, 4, 11, 18, 39, 55, 775, DateTimeKind.Utc).AddTicks(3600), false });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "IsDeleted" },
                values: new object[] { new DateTime(2023, 4, 11, 18, 39, 55, 775, DateTimeKind.Utc).AddTicks(5240), false });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "IsDeleted" },
                values: new object[] { new DateTime(2023, 4, 11, 18, 39, 55, 775, DateTimeKind.Utc).AddTicks(5270), false });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "IsDeleted" },
                values: new object[] { new DateTime(2023, 4, 11, 18, 39, 55, 775, DateTimeKind.Utc).AddTicks(5300), false });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "IsDeleted" },
                values: new object[] { new DateTime(2023, 4, 11, 18, 39, 55, 775, DateTimeKind.Utc).AddTicks(5320), false });

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
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

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
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tasqs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Accounts");

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
                value: new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(3420));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(3450));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOn", "TempId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(5160), 0 },
                    { 2, new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(5200), 0 },
                    { 3, new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(5230), 0 },
                    { 4, new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(5250), 0 }
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
