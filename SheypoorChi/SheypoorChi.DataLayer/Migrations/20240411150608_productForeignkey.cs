using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheypoorChi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class productForeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7155ccbf-9dab-4112-bbac-9680b89bc802"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("19e81c35-b8a7-47d8-afc4-89fbd5e02de2"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("150f4ee3-70a8-4811-8ef6-6b7b5f791587"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("d69cc7ff-b3f1-4c17-b3d2-abf1db2ec333"), true, new Guid("150f4ee3-70a8-4811-8ef6-6b7b5f791587"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d69cc7ff-b3f1-4c17-b3d2-abf1db2ec333"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("150f4ee3-70a8-4811-8ef6-6b7b5f791587"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("19e81c35-b8a7-47d8-afc4-89fbd5e02de2"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("7155ccbf-9dab-4112-bbac-9680b89bc802"), true, new Guid("19e81c35-b8a7-47d8-afc4-89fbd5e02de2"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });
        }
    }
}
